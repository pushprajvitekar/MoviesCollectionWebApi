using Dapper;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseMigrations.SqlServer
{
    public static class Database
    {
        public static void RunMigrations(IConfiguration configuration)
        {
            var connString= configuration["Data:DefaultConnection:ConnectionString"] ?? throw new ArgumentNullException(nameof(configuration), $"ConnectionString not defined");
#if DEBUG

            var dbName= configuration["Data:DefaultConnection:DatabaseName"] ?? throw new ArgumentNullException(nameof(configuration), $"DatabaseName not defined");
            EnsureDatabase(connString, dbName);

#endif

            using var serviceProvider = CreateServices(connString);
            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static ServiceProvider CreateServices(string connstring)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .Configure<AssemblySourceOptions>(x => x.AssemblyNames = new[] { typeof(Program).Assembly.GetName().Name })
                .ConfigureRunner(rb => rb
                    // Add SqlServer support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(connstring)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Database).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }






        public static void EnsureDatabase(string connectionString, string databaseName)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.Remove("Database");
            using var connection = new SqlConnection(builder.ConnectionString);
            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);
            if (!connection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters).Any())
            {
                connection.Execute($"CREATE DATABASE {databaseName}");
            }
        }
    }
}
