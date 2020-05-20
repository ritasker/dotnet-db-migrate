namespace Pier8.DbTools.Commands.Migrate.Migrators
{
    using System;
    using DbUp;
    using DbUp.Engine;

    public class MSSqlMigrator : DbMigrator
    {
        private readonly string connectionString;

        public MSSqlMigrator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override DatabaseUpgradeResult Migrate(string path)
        {
            try
            {
                EnsureDatabase.For.SqlDatabase(this.connectionString);
            }
            catch (Exception e)
            {
                throw new MigrationFailedException(e.Message, e);
            }
            
            var upgrader = DeployChanges.To.SqlDatabase(this.connectionString)
                .LogToConsole()
                .WithScriptsFromFileSystem(path)
                .Build();

            if (!upgrader.TryConnect(out string errorMessage))
            {
                throw new MigrationFailedException(errorMessage);
            }
            
            return upgrader.PerformUpgrade();
        }
    }
}