FROM mcr.microsoft.com/dotnet/core/sdk:2.2.202-stretch AS build
RUN apt-get update
RUN apt-get -y install zip

WORKDIR /repo

# Optimisation: Copy dotnet project files only and do a dotnet restore.
# As package references change slowly, this will improve build times
# by caching restored packages in a layer.

# Copy slns, csprojs and do a dotnet restore

COPY ./src/*.sln ./src/
COPY ./src/*/*.csproj ./src/
# This assumes the csproj name is same as it's containing directory
RUN for file in $(ls src/*.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

WORKDIR /repo/src
RUN dotnet restore
WORKDIR /repo

# Copy remaining source files
COPY ./src ./src/
COPY deploy.sh deploy.sh
COPY deploy.dockerfile deploy.dockerfile

RUN dotnet build
RUN dotnet pack -o /repo/artifacts
