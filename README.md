# DwCheckApi - .NET Core
## Description

This project is a .NET core implemented Web API for listing all of the (canon) [Discworld](https://en.wikipedia.org/wiki/Discworld#Novels) novels.

It uses Entity Framework Core to communicate with a Sqlite database, which contains a record for each of the Discworld novels.

## Building and Running

1. Change directory to the root of the code

    `cd dwCheckApi`

1. Issue the `dotnet` restore command (this resolves all NuGet packages)

    `dotnet restore`

1. Issue the `dotnet` build command

    `dotnet build`

    This step isn't fully neccessary, but I like to do build and run as separate steps.

1. Issue the `dotnet` run command

    `dotnet run`

    This will start the Kestrel webserver, load the `dwCheckApi` application and tell you, via the terminal, what the url to access `HiFive-Server` will be. Usually this will be `http://localhost:5000`, but it may be different based on your system configuration.

## Polling and Usage of the API

`dwCheckApi` currently only accepts the HTTP GET verb followed by the book number, the generated response will be the name of the book of "Not found" if it is not found.

## Seeding the Database

During startup, in the Configure method, `dwCheck` will apply any outstanding mirgrations (which is not a fantastic practise, but will be ok for now) then seeds the database via the `EnsureSeedData` extention method. This is an automatic process and requires no user input.

## Data Source

The [L-Space wiki](http://wiki.lspace.org/mediawiki/Bibliography#Novels) is currently being used to seed the database. All data is copyright to Terry Pratchett and/or Transworld Publishers no infringement was intended.