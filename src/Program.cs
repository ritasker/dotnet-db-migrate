namespace DbMigrate
{
    using DbUp.Engine;
    
    using System;
    using CommandLine;
    using Migrators;

    class Program
    {
        public static int Main(string[] args) => Parser.Default.ParseArguments<Options>(args)
            .MapResult(opts => MigrateDb(opts.ConnectionString, opts.Provider, opts.EnsureDbExits, opts.Scripts),
                _ => -1);

        private static int MigrateDb(string connectionString, string provider, bool ensureDbExits, string scripts)
        {
            var migrator = GetMigrator(provider, connectionString);
            
            if (ensureDbExits)
            {
                migrator.EnsureDatabaseExists();
            }

            DatabaseUpgradeResult result;
                
            try
            {
                result = migrator.Migrate(scripts);
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

        private static DbMigrator GetMigrator(string provider, string connectionString)
        {
            switch (provider.ToLower())
            {
                case "postgres":
                    return new PostgresMigrator(connectionString);
                default:
                    return new MSSqlMigrator(connectionString);
            }
        }

        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }
    }
}
