using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Generic.Database // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                //TODO: Allow for run of app with cli arguments, possible to run migration from azure pipeline??
            }
            else
            {


            }

            Console.WriteLine("Welcome to fluent db migrator for fbo");
            Console.WriteLine("You can force close with Ctrl + c");
            do
            {
                var direction = "";
                long version = 0;
                var con = "Data Source=localhost; Initial Catalog = Test;TrustServerCertificate=True;Persist Security Info=True;Integrated Security=SSPI";
                //var con = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=fbo_local;Integrated Security=True";

                Console.WriteLine($"Currently pointed to: {con}");
                Console.WriteLine("Please hit enter if that is acceptable or enter the new connection string");
                var input = Console.ReadLine();
                con = string.IsNullOrEmpty(input ?? "") ? con : input;





                Console.WriteLine($"Confirmed db connection: {con}");
                while ((direction ?? "").ToUpper() is not "UP" and not "DOWN")
                {
                    Console.WriteLine("Please enter a migration direction: UP/DOWN");
                    direction = Console.ReadLine();
                }

                bool breakOut = false;
                do
                {
                    Console.WriteLine("Please enter a version number to migrate to or hit enter to continue");
                    input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        breakOut = true;
                    }
                    else if (long.TryParse(input, out version))
                    {
                        breakOut = true;

                    }
                } while (!breakOut);

                var serviceProvider = CreateServices(con);
                using (var scope = serviceProvider.CreateScope())
                {
                    UpdateDatabase(scope.ServiceProvider, direction, version);
                }
                Console.WriteLine("Type Exit to close the migrator or hit enter to run another migration");
            } while ((Console.ReadLine() ?? "").ToUpper() != "EXIT");
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices(string connection)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQL support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(connection)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider, string direction, long ver = 0)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            if (direction.ToUpper() == "UP" && ver > 0)
            {
                runner.MigrateUp(ver);
            }
            if (direction.ToUpper() == "UP" && ver == 0)
            {
                runner.MigrateUp();
            }
            if (direction.ToUpper() == "DOWN" && ver > 0)
            {
                runner.MigrateDown(ver);
            }
            if (direction.ToUpper() == "DOWN" && ver == 0)
            {
                runner.MigrateDown(ver);
            }

        }
    }
}
