namespace DbMigrate.Commands.Migrate
{
    using System;
    using System.Data.Common;
    using FluentValidation;
    using Migrate;

    public class MigrateValidator : AbstractValidator<MigrateCommand>
    {
        public MigrateValidator()
        {
            RuleFor(x => x.ConnectionString)
                .NotNull().WithMessage("connection string is required.")
                .Must(BeAConnectionString).WithMessage("connection string does not conform to specification");
        }

        private bool BeAConnectionString(string connectionString)
        {
            try
            {
                new DbConnectionStringBuilder().ConnectionString = connectionString;
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}