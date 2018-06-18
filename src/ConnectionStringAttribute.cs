namespace db_migrate
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Common;

    public class ConnectionStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var builder = new DbConnectionStringBuilder();
            
            try
            {
                builder.ConnectionString = value.ToString();
            }
            catch (ArgumentException)
            {
                return new ValidationResult("An invalid connection string was provided.");
            }

            return ValidationResult.Success;
        }
    }
}