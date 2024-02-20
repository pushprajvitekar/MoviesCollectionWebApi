using DatabaseMigrations.SqlServer;
using Microsoft.Extensions.Configuration;

namespace DatabaseMigrations
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Migrator...");
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();
                Database.RunMigrations(configuration);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception:{e}");
            }
        }

    }
}
