namespace Pier8.DbTools.Commands.Migrate.Migrators
{
    using DbUp.Engine;

    public abstract class DbMigrator
    {
        public abstract DatabaseUpgradeResult Migrate(string path);
    }
}