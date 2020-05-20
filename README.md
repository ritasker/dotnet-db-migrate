# Pier 8 DB Tools

[![Build status](https://ci.appveyor.com/api/projects/status/1rjkqrumqq52yfdj?svg=true)](https://ci.appveyor.com/project/ritasker/dotnet-db-migrate)
[![NuGet](https://img.shields.io/nuget/v/dotnet-db-migrate.svg)](https://www.nuget.org/packages/dotnet-db-migrate/)
[![NuGet](https://img.shields.io/myget/ritasker/v/dotnet-db-migrate.svg)](https://www.myget.org/feed/ritasker/package/nuget/dotnet-db-migrate/)


A CLI that adds and migrates database migrations for MS SQL and PostgreSQL.

## Quick Start

### Installation

`dotnet tool install -g dotnet-db-migrate`

## Example Usage

`dotnet db add-migration "AddContactsTable"`

```bash
Added migration script ./20200520180400_AddContactsTable.sql
```

You would then add the SQL for the migration. 

eg, for PostgreSQL.

```postgresql
CREATE EXTENSION pgcrypto;
CREATE TABLE contacts(
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  name TEXT,
  email TEXT
);
```

Then you can execute the migration using the following command,

`dotnet db migrate "Server=localhost;Port=5432;Database=db-migrate;User Id=postgres;Password=password;" -p psql`

```bash
Master ConnectionString => Host=localhost;Port=5432;Database=db-migrate;Username=postgres;Password=********
Created database db-migrate
Beginning database upgrade
Checking whether journal table exists..
Journal table does not exist
Executing Database Server script '20200520180400_AddContactsTable.sql'
Checking whether journal table exists..
Creating the "schemaversions" table
The "schemaversions" table has been created
Upgrade successful
```

## Contributing

This is an open-source project. I request your participation through issues and pull requests!

## License

dotnet-db-migrate is licensed under MIT. Refer to [LICENSE](https://github.com/ritasker/dotnet-db-migrate/blob/master/LICENSE) for more information.

