namespace Pier8.DbTools
{
    using Commands.AddMigration;
    using Commands.Migrate;
    using McMaster.Extensions.CommandLineUtils;

    [Command(Name = "db", Description = "A CLI that adds and migrates database migrations for MS SQL and PostgreSQL."),
     Subcommand(typeof(MigrateCommand), typeof(AddMigrationCommand))]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);
    }
}
