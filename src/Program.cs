namespace db_migrate
{
    using McMaster.Extensions.CommandLineUtils;
    
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Common;
    using DbUp.Engine;

    [Command(Description = "A tool to deploy changes to SQL databases.")]
    [HelpOption("-h|--help")]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Required]
        [Option(Description = "Required. The connection details for a database.")]
        private string ConnectionString { get; }

        [Option(Template = "-p|--provider",
            Description = "Optional. The connection provider. Default: mssql.")]
        [AllowedValues("mssql", "azure", "sqlite", "sqlce", "mysql", "postgres", "firebird", IgnoreCase = true)]
        private string Provider { get; } = "mssql";

        [Option(Template = "-s|--scripts",
            Description = "Optional. The path to the migration scripts. Default: scripts/")]
        private string Scripts { get; } = "scripts";

        [Option(Template = "--ensure-db-exists", Description = "Optional. Create the database if it doesn't exist. Default: false")]
        private bool EnsureDatabaseExists { get; } = false;

        private int OnExecute()
        {
            if (!ValidateConnectionString(ConnectionString))
            {
                WriteError("An invalid connection string was provided.");
                return 1;
            }

            DbMigrator migrator = new PostgresMigrator(ConnectionString);

            if (EnsureDatabaseExists)
            {
                migrator.EnsureDatabaseExists();
            }

            DatabaseUpgradeResult result;
                
            try
            {
                result = migrator.Migrate(Scripts);
            }
            catch (ConnectionFailedException e)
            {
                WriteError($"{e.Message}{Environment.NewLine}Please check the connection string for errors or use the --ensure-db-exists flag to create the db.");
                return 1;
            }

            if (!result.Successful)
            {
                WriteError(result.Error.Message);
                return 1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }

        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }

        private bool ValidateConnectionString(string connectionString)
        {
            var builder = new DbConnectionStringBuilder();

            try
            {
                builder.ConnectionString = connectionString;
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}
