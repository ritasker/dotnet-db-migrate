namespace DbMigrate.Migrators
{
    using DbUp;
    using DbUp.Engine;

    public class MSSqlMigrator : DbMigrator
    {
        private readonly string connectionString;

        public MSSqlMigrator(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public override void EnsureDatabaseExists()
        {
            EnsureDatabase.For.SqlDatabase(connectionString);
        }

        public override DatabaseUpgradeResult Migrate(string path)
        {
            var upgrader = DeployChanges.To.SqlDatabase(connectionString)
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