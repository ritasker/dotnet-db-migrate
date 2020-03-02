namespace DbMigrate
{
    using System.Security.Principal;
    using CommandLine;

    public class Options
    {
        [Option('c', Required = true)]
        public string ConnectionString { get; set; }

        [Option('p')]
        public string Provider { get; set; }

        [Option('s')]
        public string Scripts { get; set; }

        [Option("ensure-db-exists")]
        public bool EnsureDbExits { get; set; }
    }
}