namespace DbMigrate.Commands.Migrate
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using McMaster.Extensions.CommandLineUtils;
    using Migrators;

    [Command("migrate", Description = "Runs the migration scripts")]
    public class MigrateCommand
    {
        [Required(ErrorMessage = "connection string is required.")]
        [ConnectionString]
        [Option(Description = "The connection string for the database")]
        public string ConnectionString { get; }
        
        [Option(Description = "The path to the migration scripts.")]
        private string Scripts { get; } = ".";

        [Option(Description="The connection provider.")]
        private string Provider { get; } = "mssql";
        
        public int OnExecute(IConsole console)
        {
            try
            {
                var migrator = GetMigrator(Provider, ConnectionString);
                var result = migrator.Migrate(Scripts);
                    
                if (!result.Successful)
                {
                    WriteError(console, $"{result.Error.Message} for provider {Provider}");
                    return 1;
                }

                return 0;
            }
            catch (ConnectionFailedException e)
            {
                WriteError(console, $"{e.Message} for provider {Provider}");
                return 1;
            }
        }

        private void WriteError(IConsole console, string message)
        {
            console.ForegroundColor = ConsoleColor.DarkRed;
            console.Error.WriteLine(message);
            console.ResetColor();
        }
        
        private static DbMigrator GetMigrator(string provider, string connectionString)
        {
            switch (provider.ToLower())
            {
                case "pgsql":
                    return new PostgresMigrator(connectionString);
                case "mssql":
                    return new MSSqlMigrator(connectionString);
                default:
                    throw new ArgumentOutOfRangeException(nameof(provider), provider);
            }
        }
    }
}