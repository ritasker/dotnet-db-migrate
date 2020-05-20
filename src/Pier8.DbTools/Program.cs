namespace Pier8.DbTools
{
    using Commands.AddMigration;
    using Commands.Migrate;
    using McMaster.Extensions.CommandLineUtils;

    [Command(Name = "db", Description = "A .NET Core Global Tool to deploy changes to SQL databases."),
     Subcommand(typeof(MigrateCommand), typeof(AddMigrationCommand))]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);
    }
}
