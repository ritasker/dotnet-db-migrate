# Db-Migrate

[![Build status](https://ci.appveyor.com/api/projects/status/51x073rgmxhyopaf/branch/master?svg=true)](https://ci.appveyor.com/project/ritasker/db-migrate/branch/master)
[![NuGet](https://img.shields.io/nuget/v/db-migrate.svg)](https://www.nuget.org/packages/db-migrate/)

A [.NET Core Global Tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) to deploy changes to SQL databases using [DbUp](https://github.com/DbUp/DbUp).

## Installation

`dotnet tool install -g db-migrate`

At this point you will probably have to add the global tools path to `PATH` variable.
Global tools are installed to `%USERPROFILE%\.dotnet\tools` (Windows) or `$HOME/.dotnet/tools` (macOS/Linux).
You can add the tools directory to yur `PATH` using one of the following commands.

Windows:
`setx PATH "$env:PATH;$env:USERPROFILE/.dotnet/tools"`
Linux/macOS:
`echo "export PATH=\"\$PATH:\$HOME/.dotnet/tools\"" >> ~/.bashrc` or your terminal of choice.

You will need to restart your terminal.


## Usage

Once installed, running `db-migrate -h` will print the following help information.

```bash
A tool to deploy changes to SQL databases.

Usage: db-migrate [arguments] [options]

Arguments:
  ConnectionString    Required. The connection details for a database.

Options:
  -h|--help           Show help information
  -p|--provider       Optional. The connection provider. Default: mssql
  -s|--scripts        Optional. The path to the migration scripts. Default: scripts/
  --ensure-db-exists  Optional. Create the database if it doesn't exist. Default: false
```

### Example Usage

`db-migrate "Server=localhost;Port=5432;Database=northwind;User Id=postgres;Password=password;" -p postgres -s src/scripts/Postgres --ensure-db-exists`

#### Output
```bash
Master ConnectionString => Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=********
Created database northwind
Beginning database upgrade
Checking whether journal table exists..
Journal table does not exist
Executing Database Server script '201806172131-Create-Contacts.sql'
Checking whether journal table exists..
Creating the "schemaversions" table
The "schemaversions" table has been created
Upgrade successful
Success!
```
Currently only MS SQL Server and Postgres are supported, this is due to `--ensure-db-exists` flag and how DbUp creates databases.

## Contributing

This is an open-source project. I request your participation through issues and pull requests!

## License

db-migrate is licensed under MIT. Refer to [LICENSE](https://github.com/ritasker/db-migrate/blob/master/LICENSE) for more information.

