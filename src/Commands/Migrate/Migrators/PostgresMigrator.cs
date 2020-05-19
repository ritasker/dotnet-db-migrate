namespace DbMigrate.Commands.Migrate.Migrators
{
    using Migrate;
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
            var upgrader = DeployChanges.To.PostgresqlDatabase(connectionString)
                .LogToConsole()
                .WithScriptsFromFileSystem(path)
                .Build();

            if (!upgrader.TryConnect(out string errorMessage))
            {
                throw new ConnectionFailedException(errorMessage);
            }
            
            EnsureDatabase.For.PostgresqlDatabase(connectionString);
            return upgrader.PerformUpgrade();
        }
    }
}