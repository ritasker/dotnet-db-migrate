namespace DbMigrate.Commands.Migrate
{
    using System;
    public class ConnectionFailedException : Exception
    {
        public ConnectionFailedException(string message) : base(message)
        {   
        }
    }
}