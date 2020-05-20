namespace Pier8.DbTools.Commands.Migrate
{
    using System;

    public class MigrationFailedException : Exception
    {
        public MigrationFailedException(string message) : base(message)
        {
        }
        
        public MigrationFailedException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}