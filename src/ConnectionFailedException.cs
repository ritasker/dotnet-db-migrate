namespace DbMigrate
{
    using System;
    public class ConnectionFailedException : Exception
    {
        public ConnectionFailedException(string message) : base(message)
        {   
        }
    }
}