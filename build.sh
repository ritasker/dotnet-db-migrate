#!/usr/bin/env bash
set -e

dotnet restore
dotnet build -c Release
dotnet pack -o $PWD/artifacts