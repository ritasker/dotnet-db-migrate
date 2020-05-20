namespace DbMigrate.Commands.Migrate
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Common;

    public class ConnectionStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                new DbConnectionStringBuilder().ConnectionString = value.ToString();
                return ValidationResult.Success;
            }
            catch (ArgumentException)
            {
                return new ValidationResult("connection string does not conform to specification");
            }
        }
    }
}