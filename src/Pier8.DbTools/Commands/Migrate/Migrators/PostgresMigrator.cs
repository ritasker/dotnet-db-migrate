namespace Pier8.DbTools.Commands.Migrate.Migrators
{
    using System;
    using DbUp;
    using DbUp.Engine;

    public class PostgresMigrator : DbMigrator
    {
        private readonly string connectionString;

        public PostgresMigrator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override DatabaseUpgradeResult Migrate(string path)
        {
            try
            {
                EnsureDatabase.For.PostgresqlDatabase(this.connectionString);
            }
            catch (Exception e)
            {
                throw new MigrationFailedException(e.Message, e);
            }
            
            var upgrader = DeployChanges.To.PostgresqlDatabase(this.connectionString)
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