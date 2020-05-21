#!/usr/bin/env bash
set -e

dotnet restore
dotnet build -c Release
dotnet pack -o $PWD/artifacts

grep '<Version>' < ./src/Pier8.DbTools/*.csproj | sed 's/.*<Version>\(.*\)<\/Version>/\1/' > $PWD/artifacts/version.txt