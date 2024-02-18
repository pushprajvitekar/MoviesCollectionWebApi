using DatabaseMigrations.SqlServer;

namespace DatabaseMigrations
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Migrator...");
            try
            {
                Database.RunMigrations();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception:{e}");
            }
        }

    }
}
