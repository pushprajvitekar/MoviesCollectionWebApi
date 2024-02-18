using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DatabaseMigrations.SqlServer
{
    public static class Database
    {
        private const string ConnectionString = "Server=.\\SQLEXPRESS2022;Database=MovieCollectionApi;Trusted_Connection=True;MultipleActiveResultSets=true; Encrypt=false";
        public static void RunMigrations()
        {
#if DEBUG

            EnsureDatabase("Persist Security Info = False; Integrated Security = false; Initial Catalog = master;TrustServerCertificate=True;Trusted_Connection=true;server = .\\SQLEXPRESS2022", "MovieCollectionApi");

#endif

            using var serviceProvider = CreateServices();
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
        private static ServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .Configure<AssemblySourceOptions>(x => x.AssemblyNames = new[] { typeof(Program).Assembly.GetName().Name })
                .ConfigureRunner(rb => rb
                    // Add SqlServer support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(ConnectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Database).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }






        public static void EnsureDatabase(string connectionString, string databaseName)
        {
            using var connection = new SqlConnection(connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("name", databaseName);
            if (!connection.Query("SELECT * FROM sys.databases WHERE name = @name", parameters).Any())
            {
                connection.Execute($"CREATE DATABASE {databaseName}");
            }
        }
    }
}
