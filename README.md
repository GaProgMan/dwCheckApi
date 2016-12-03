# DwCheckApi - .NET Core
## Description

This project is a .NET core implemented Web API for listing all of the (canon) [Discworld](https://en.wikipedia.org/wiki/Discworld#Novels) novels.

It uses Entity Framework Core to communicate with a Sqlite database, which contains a record for each of the Discworld novels.

It has been released, as is, using a GPL v3 licence. For more information on the GPL V3 licence, please see either the `LICENSE` file in the root of the repository or see the tl;dr Legal page for [GPL V3](https://tldrlegal.com/license/gnu-general-public-license-v3-(gpl-3))

## Building and Running

1. Change directory to the root of the code

    `cd dwCheckApi`

1. Issue the `dotnet` restore command (this resolves all NuGet packages)

    `dotnet restore`

1. Issue the `dotnet` build command

    `dotnet build`

    This step isn't fully neccessary, but I like to do build and run as separate steps.

    *Note: this step explicitly builds a debug version of dwCheckApi. See the sub section on Build Information for more information.*

1. Issue the `dotnet` run command

    `dotnet run`

    This will start the Kestrel webserver, load the `dwCheckApi` application and tell you, via the terminal, what the url to access `dwCheckApi` will be. Usually this will be `http://localhost:5000`, but it may be different based on your system configuration.


### .NET Core Build Information

The following commands can be used to run different versions of dwCheckApi

    dotnet bin/Debug/netcoreapp1.0/dwCheckApi.dll (for the debug build)

    dotnet bin/Release/netcoreapp1.0/dwCheckApi.dll (for the release build)

This set of commands explicitly inform the .NET Core runtime which build of the server we would like to run.

If you do not explicitly provide a configuration in the build step, then the debug version of dwCheckApi will be built. If you explicitly request a Release build, but issue `dotnet run`, then a debug version of dwCheckApi will be built.

To request a Release build of dwCheckApi, then the following command should produce one:

    dotnet build --configuration Release

## Entities

`dwCheckApi` has the following entities:

1. Book

   The `Book` entity represents each of the novels in the Discworld main canon. As the time of writing, this does not include any of the "Science of Discworld" titles.

   The `Book` entity contains the following fields:

   | Property          | Type        | Description          |
   | ------------------| ------------|----------------------|
   | BookId            | int         | Primary key          |
   | BookOrdinal       | int         | Release Order        |
   | BookName          | string      | Book Name |
   | BookDescription   | string      | Book description (taken from the back of the book) |
   | BookIsbn10        | string      | ISBN 10 of the book  |
   | BookIsbn13        | string      | ISBN 13 of the book  |
   | BookCoverImage    | byte array  | Represents the image for the cover art |
   | BookCoverImageUrl | string      | is a URL to the cover art (to be used as a backup, if BookCoverImage is null) |

1. Character

   The `Character` entity represents each of the characters in the Discworld, and contains only two fields:

   | Property          | Type        | Description          |
   | ------------------| ------------|----------------------|
   | CharacterId       | int         | Primary key          |
   | CharacterName     | string      | Character's Name     |

## View Models

The data returned from `dwCheck`'s server is in a slightly different, more simplified format, than that used for the Database Models.

1. Book

   The `Book` view model represents the data found in the `Book` database model and takes a similar format. However any linked characters are inlcuded in an array.

   The `Book` view model contains the following fields:

   | Property          | Type             | Description          |
   | ------------------| -----------------|----------------------|
   | BookId            | int              | Primary key          |
   | BookOrdinal       | int              | Release Order        |
   | BookName          | string           | Book Name |
   | BookDescription   | string           | Book description (taken from the back of the book) |
   | BookIsbn10        | string           | ISBN 10 of the book  |
   | BookIsbn13        | string           | ISBN 13 of the book  |
   | BookCoverImage    | byte array       | Represents the image for the cover art |
   | BookCoverImageUrl | string           | is a URL to the cover art (to be used as a backup, if BookCoverImage is null) |
   | Characters        | array of strings | an array, containing 0 or more entries, of character name strings |

1. Character

   The `Character` view model represents the data found in the `Character` database model and takes a similar format. However any linked books are included in an array.
   
   The `Character` view model contains the following fields:

   | Property          | Type             | Description          |
   | ------------------| -----------------|----------------------|
   | CharacterName     | string           | Character's Name     |
   | Books             | array of strings | An array, containing 0 or more entires, of book title strings |


## Polling and Usage of the API

`dwCheckApi` has the following Controllers:

1. Books

    The `Books` controller has two methods:

    1. Get

        The `Get` action takes an integer Id. This field represents the ordinal for the novel. This ordinal is based on release order, so if the user want data on 'Night Watch', they would set a GET request to:

            /Books/Get/29
        
        This will return the following JSON data:

            {
                "bookId":8,
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

# Seeding the Database

During startup, in the Configure method, `dwCheck` will apply any outstanding mirgrations (which is not an acceptable best practise, but is ok for during development. This feature will be disabled once the code is ready for release) then seeds the database via the `EnsureSeedData` extention method, if there is no data.

*Note: Seeding of the initial dataset will not happen if deCheckApi detects a pre-existing database. However, migrations may still be applied*

This is an automatic process and requires no user input.

# Data Source

The [L-Space wiki](http://wiki.lspace.org/mediawiki/Bibliography#Novels) is currently being used to seed the database.

All character and book data are copyrighted to Terry Pratchett and/or Transworld Publishers no infringement was intended.

# TODO

Strikethrough or remove when done

- Update README sections for models and returned data types