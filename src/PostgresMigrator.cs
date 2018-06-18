namespace db_migrate
{
    using DbUp;
    using DbUp.Engine;
    
    using System;

    public class PostgresMigrator : DbMigrator
    {
        private readonly string connectionString;

        public PostgresMigrator(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public override void EnsureDatabaseExists()
        {
            EnsureDatabase.For.PostgresqlDatabase(connectionString);
        }

        public override DatabaseUpgradeResult Migrate(string path)
        {   
            var upgrader = DeployChanges.To.PostgresqlDatabase(connectionString)
                .LogToConsole()
                .WithScriptsFromFileSystem(path)
                .Build();

            if (!upgrader.TryConnect(out string errorMessage))
            {
                throw new ConnectionFailedException(errorMessage);
            }
            
            return upgrader.PerformUpgrade();
        }
    }
}