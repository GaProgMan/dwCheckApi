# DwCheckApi - .NET Core

## Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/6yojesrvj7d4q51c?svg=true)](https://ci.appveyor.com/project/GaProgMan/dwcheckapi)

## Live Deployment Status
[![Deployment status](https://ci.appveyor.com/api/projects/status/f2hk3k5hvl609jow?svg=true)](https://ci.appveyor.com/project/GaProgMan/dwcheckapi-c8vm5)

## Licence
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Code Triage Status
[![Code Triagers Badge](https://www.codetriage.com/gaprogman/dwcheckapi/badges/users.svg)](https://www.codetriage.com/gaprogman/dwcheckapi)

## Description

This project is a .NET core implemented Web API for listing all of the (canon) [Discworld](https://en.wikipedia.org/wiki/Discworld#Novels) novels.

It uses Entity Framework Core to communicate with a Sqlite database, which contains a record for each of the Discworld novels.

It has been released, as is, using an MIT licence. For more information on the MIT licence, please see either the `LICENSE` file in the root of the repository or see the tl;dr Legal page for [MIT](https://tldrlegal.com/license/mit-license)

## Code of Conduct
dwCheckApi has a Code of Conduct which all contributors, maintainers and forkers must adhere to. When contributing, maintaining, forking or in any other way changing the code presented in this repository, all users must agree to this Code of Conduct.

See `Code of Conduct.md` for details.

## Pull Requests

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

Pull requests are welcome, but please take a moment to read the Code of Conduct before submitting them or commenting on any work in this repo.

## Creating the Database

This will need to be perfored before running the application for the first time

1. Change to the Persistence directory (i.e. `dwCheckApi/dwCheckApi.Persistence`)

    `cd dwCheckApi.Persistence`

1. Issue the Entity Framework command to update the database

    `dotnet ef database update`

    This will ensure that all migrations are used to create or alter the local database instance, ready for seeding (see `Seeding the Database`)

## Building and Running

1. Change to the api directory (i.e. `dwCheckApi/dwCheckApi`)

    `cd dwCheckApi`

1. Issue the `dotnet` restore command (this resolves all NuGet packages)

    `dotnet restore`

1. Issue the `dotnet` build command

    `dotnet build`

1. Issue the `dotnet` run command

    `dotnet run`

    This will start the Kestrel webserver, load the `dwCheckApi` application and tell you, via the terminal, what the url to access `dwCheckApi` will be. Usually this will be `http://localhost:5000`, but it may be different based on your system configuration.

## Seeding the Database

There are a series of API endpoints related to clearing and seeding the database. These can be found at:

    /Database/DropData
    /Database/SeedData

These two commands (used in conjunction with each other) will drop all data from the database, then seed the database (respectively) from a series of JSON files that can be found in the `SeedData` directory.

`dwCheckApi` has been designed so that the user can add as much data as they like via the JSON files. This means that `dwCheckApi` is not limited to Discworld novels and characters.

A user of this API could alter the JSON files, drop the data and reseed and have a completely different data set - perhaps Stephen King novels, for example.

## Testing

This repository contains an xUnit.NET test library. To run the tests:

1. Change directory to the tests directory

    `cd dwCheckApi.Tests`

1. Issue the `dotnet` restore command (this resolves all NuGet packages)

    `dotnet restore`

1. Issue the `xunit` command

    `dotnet xunit`

    All tests will be run against a new build of `dwCheckApi` and results will be returned in the open shell/command prompt window.

## Polling and Usage of the API

`dwCheckApi` has the following Controllers:

1. Books

    The `Books` controller has two methods:

    1. Get

        The `Get` action takes an integer Id. This field represents the ordinal for the novel. This ordinal is based on release order, so if the user want data on 'Night Watch', they would set a GET request to:

            /Books/Get/29
        
        This will return the following JSON data:

            {
                "bookOrdinal":29,
                "bookName":"Night Watch",
                "bookIsbn10":"0552148997",
                "bookIsbn13":"9780552148993",
                "bookDescription":"This morning, Commander Vimes of the City Watch had it all. He was a Duke. He was rich. He was respected. He had a titanium cigar case. He was about to become a father. This morning he thought longingly about the good old days. Tonight, he's in them.",
                "bookCoverImage":null,
                "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/4/4f/Cover_Night_Watch.jpg",
                "characters" :
                    [
                        "Fred Colon",
                        "Nobby Nobbs",
                        "Rosie Palm",
                        "Samuel Vimes",
                        "The Patrician"
                    ]
            }

    1. Search

        The `Search` action takes a string parameter called `searchString`. `dwCheckApi` will search the following fields of all Book records and return once which have any matches:

        - BookName
        - BookDescription
        - BookIsbn10
        - BookIsbn13

        If the user wishes to search for the prase "Rincewind", then they should issue the following request:

            /Books/Search?searchString=Rincewind
        
        This will return the following JSON data:

            [
              {
                  "bookId":23,
                  "bookOrdinal":2,
                  "bookName":"The Light Fantastic",
                  "bookIsbn10":"0861402030",
                  "bookIsbn13":"9780747530794",
                  "bookDescription":"As it moves towards a seemingly inevitable collision with a malevolent red star, the Discworld has only one possible saviour. Unfortunately, this happens to be the singularly inept and cowardly wizard called Rincewind, who was last seen falling off the edge of the world ....",
                  "bookCoverImage":null,
                  "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/f/f1/Cover_The_Light_Fantastic.jpg",
                  "characters":
                    [
                        "The Lady",
                        "Rincewind",
                        "The Partician",
                        "The Luggage",
                        "Blind Io",
                        "Fate",
                        "Death",
                        "Twoflower",
                        "Offler",
                        "Ridcully"
                    ]
              },
              {
                  "bookId":30,
                  "bookOrdinal":9,
                  "bookName":"Eric",
                  "bookIsbn10":"0575046368",
                  "bookIsbn13":"9780575046368",
                  "bookDescription":"Eric is the Discworld's only demonology hacker. Pity he's not very good at it. All he wants is three wishes granted. Nothing fancy - to be immortal, rule the world, have the most beautiful woman in the world fall madly in love with him, the usual stuff. But instead of a tractable demon, he calls up Rincewind, probably the most incompetent wizard in the universe, and the extremely intractable and hostile form of travel accessory known as the Luggage. With them on his side, Eric's in for a ride through space and time that is bound to make him wish (quite fervently) again - this time that he'd never been born.",
                  "bookCoverImage":null,
                  "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/2/27/Cover_Eric_%28alt%29.jpg",
                  "characters" : []
              },
              {
                  "bookId":38,
                  "bookOrdinal":17,
                  "bookName":"Interesting Times",
                  "bookIsbn10":"0552142352",
                  "bookIsbn13":"9780552142359",
                  "bookDescription":"Mighty Battles! Revolution! Death! War! (and his sons Terror and Panic, and daughter Clancy). The oldest and most inscrutable empire on the Discworld is in turmoil, brought about by the revolutionary treatise What I Did On My Holidays. Workers are uniting, with nothing to lose but their water buffaloes. Warlords are struggling for power. War (and Clancy) are spreading through the ancient cities. And all that stands in the way of terrible doom for everyone is: Rincewind the Wizzard, who can't even spell the word 'wizard' ... Cohen the barbarian hero, five foot tall in his surgical sandals, who has had a lifetime's experience of not dying ...and a very special butterfly.",
                  "bookCoverImage":null,
                  "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/9/96/Cover_Interesting_Times.jpg",
                  "characters" : []
              }
            ]
       

1. Characters

   The `Characters` controller has two methods:

   1. Get

    The `Get` action takes an integer Id. This field represents the id of the character entry in the database. It is not recommended that a consumer of this api uses this controller method, as the id entry relies entirely on the order in which Entity Framework Core persists the entries to the database while creating the dataset, and this is unpredictable. It is included here for completeness, and will probably be removed in a later version. 
            
    This ordinal is based on release order, so if the user want data on 'Night Watch', they would set a GET request to:

            /Characters/Get/4
    
    This will return JSON data similar to this one (see above for why the specific character entity may not be the same when running on a newly created database):

            {
                "characterName":"The Luggage",
                "books":
                    [
                        "The Colour of Magic"
                    ]
            }
    
   1. Search

   The `Search` action takes a string parameter called `searchString`. `dwCheckApi` will search the names of all Character records, and return those which match.

   If the user wishes to search for the prase "ri", then they should issue the following request:

        /Characters/Search?searchString=ri
        
   This will return the following JSON data:

        [
            {
                "characterName":"Ridcully",
                "books":
                [
                    "The Colour of Magic"
                ]
            },
            {
                "characterName":"Rincewind",
                "books":
                [
                    "The Colour of Magic"
                ]
            }
        ]

# Data Source

The [L-Space wiki](http://wiki.lspace.org/mediawiki/Bibliography#Novels) is currently being used to seed the database.

All character and book data are copyrighted to Terry Pratchett and/or Transworld Publishers no infringement was intended.

## A Note on the JSON files

In the SeedData directory, there are a collection of JSON files. The data source for these files is a combination of the L-Space Wiki (mentioned above) and y own knowledge of the Discworld series.

I have not altered any data from the L-Space Wiki in any way when transforming it into the JSON files. As such, the L-Space Wiki license (which is a Creative Commons Attribution ShareAlike 3.0 license) still applies.

For more information on the license used by the L-Space Wiki, please see the `Data License.md` file.
