namespace Pier8.DbTools.Commands.AddMigration
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using McMaster.Extensions.CommandLineUtils;

    [Command("add-migration", Description = "Adds an empty migration scripts")]
    public class AddMigrationCommand
    {
        [Required]
        [Argument(0, Description = "Description of the migration")]
        public string Description { get; set; }

        [Option(Description = "Where to save the migration")]
        public string Output { get; set; } = ".";

        public int OnExecute(IConsole console)
        {
            Description = Description.Trim();
            
            if (Description.Contains(" "))
            {
                Description = Description.Replace(" ", "_");
            }

            var date = DateTime.UtcNow.ToString("yyyyMMddHHmm");
            var path = Path.Combine(Output, $"{date}_{Description}.sql");
            File.Create(path);

            console.WriteLine("Added migration script {0}", path);
            return 0;
        }
    }
}