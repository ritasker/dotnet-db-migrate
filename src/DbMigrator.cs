namespace db_migrate
{
    using DbUp.Engine;

    public abstract class DbMigrator
    {
        public abstract void EnsureDatabaseExists();
        public abstract DatabaseUpgradeResult Migrate(string path);
    }
}