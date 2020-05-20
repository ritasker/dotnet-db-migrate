namespace Pier8.DbTools.Commands.Migrate
{
    using System;

    public class ConnectionFailedException : Exception
    {
        public ConnectionFailedException(string message) : base(message)
        {   
        }
    }
}