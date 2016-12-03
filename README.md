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

1. Issue the `dotnet` run command

    `dotnet run`

    This will start the Kestrel webserver, load the `dwCheckApi` application and tell you, via the terminal, what the url to access `dwCheckApi` will be. Usually this will be `http://localhost:5000`, but it may be different based on your system configuration.

## Classes

`dwCheckApi` has the following classes:

1. Book

   The `Book` class represents each of the novels in the Discworld main canon. As the time of writing, this does not include any of the "Science of Discworld" titles.

   The `Book` class contains the following fields:

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

   The `Character` class represents each of the characters in the Discworld, and contains only two fields:

   | Property          | Type        | Description          |
   | ------------------| ------------|----------------------|
   | CharacterId       | int         | Primary key          |
   | CharacterName     | string      | Character's Name     |

## Polling and Usage of the API

`dwCheckApi` has the following Controllers:

1. Books

    The `Books` controller has three methods:

    1. All

    It is not recommended to use this action, it is included for testing purposes only. It is not recommended that users have access to this controller method as it will return ALL Book data in the system - which could be expensive both computationally and with refards to bandwidth (once the server is hosted).

    The `All` action takes no parameters and returns all Book records, ordered by ordinal (which is based on release order). If the user wishes to request all data, they can do so by issuing the folllowing request:

        /Books/All
    
    This will return a lot of data, a small sample of which is presented here as an example:

        [
            {
                "bookId":1,
                "bookOrdinal":1,
                "bookName":"The Colour of Magic",
                "bookIsbn10":"086140324X",
                "bookIsbn13":"9780552138932",
                "bookDescription":"On a world supported on the back of a giant turtle (sex unknown), a gleeful, explosive, wickedly eccentric expedition sets out. There's an avaricious but inept wizard, a naive tourist whose luggage moves on hundreds of dear little legs, dragons who only exist if you believe in them, and of course THE EDGE of the planet ...",
                "bookCoverImage":null,
                "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/c/c9/Cover_The_Colour_Of_Magic.jpg"
            },
            {
                "bookId":23,
                "bookOrdinal":2,
                "bookName":"The Light Fantastic",
                "bookIsbn10":"0861402030",
                "bookIsbn13":"9780747530794",
                "bookDescription":"As it moves towards a seemingly inevitable collision with a malevolent red star, the Discworld has only one possible saviour. Unfortunately, this happens to be the singularly inept and cowardly wizard called Rincewind, who was last seen falling off the edge of the world ....",
                "bookCoverImage":null,
                "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/f/f1/Cover_The_Light_Fantastic.jpg"
            },
            ... many records removed
            {
                "bookId":20,
                "bookOrdinal":40,
                "bookName":"Raising Steam",
                "bookIsbn10":"0857522272",
                "bookIsbn13":"9780857522276",
                "bookDescription":"To the consternation of the patrician, Lord Vetinari, a new invention has arrived in Ankh-Morpork - a great clanging monster of a machine that harnesses the power of all of the elements: earth, air, fire and water. This being Ankh-Morpork, it's soon drawing astonished crowds, some of whom caught the zeitgeist early and arrive armed with notepads and very sensible rainwear. Moist von Lipwig is not a man who enjoys hard work - as master of the Post Office, the Mint and the Royal Bank his input is, of course, vital... but largely dependent on words, which are fortunately not very heavy and don't always need greasing. However, he does enjoy being alive, which makes a new job offer from Vetinari hard to refuse... Steam is rising over Discworld, driven by Mister Simnel, the man wi' t'flat cap and sliding rule who has an interesting arrangement with the sine and cosine. Moist will have to grapple with gallons of grease, goblins, a fat controller with a history of throwing employees down the stairs and some very angry dwarfs if he's going to stop it all going off the rails...",
                "bookCoverImage":null,
                "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/1/1b/RaisingSteam.jpg"
            },
            {
                "bookId":41,
                "bookOrdinal":41,
                "bookName":"The Shepheard's Crown",
                "bookIsbn10":"0857534815",
                "bookIsbn13":"9780857534811",
                "bookDescription":"A SHIVERING OF WORLDS.Deep in the Chalk, something is stirring. The owls and the foxes can sense it, and Tiffany Aching feels it in her boots. An old enemy is gathering strength. This is a time of endings and beginnings, old friends and new, a blurring of edges and a shifting of power. Now Tiffany stands between the light and the dark, the good and the bad. As the fairy horde prepares for invasion, Tiffany must summon all the witches to stand with her. To protect the land. Her land. There will be a reckoning ...",
                "bookCoverImage":null,
                "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/b/b6/Tsc.jpg"
            }
        ] 

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
                "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/4/4f/Cover_Night_Watch.jpg"
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
                  "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/f/f1/Cover_The_Light_Fantastic.jpg"
              },
              {
                  "bookId":30,
                  "bookOrdinal":9,
                  "bookName":"Eric",
                  "bookIsbn10":"0575046368",
                  "bookIsbn13":"9780575046368",
                  "bookDescription":"Eric is the Discworld's only demonology hacker. Pity he's not very good at it. All he wants is three wishes granted. Nothing fancy - to be immortal, rule the world, have the most beautiful woman in the world fall madly in love with him, the usual stuff. But instead of a tractable demon, he calls up Rincewind, probably the most incompetent wizard in the universe, and the extremely intractable and hostile form of travel accessory known as the Luggage. With them on his side, Eric's in for a ride through space and time that is bound to make him wish (quite fervently) again - this time that he'd never been born.",
                  "bookCoverImage":null,
                  "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/2/27/Cover_Eric_%28alt%29.jpg"
              },
              {
                  "bookId":38,
                  "bookOrdinal":17,
                  "bookName":"Interesting Times",
                  "bookIsbn10":"0552142352",
                  "bookIsbn13":"9780552142359",
                  "bookDescription":"Mighty Battles! Revolution! Death! War! (and his sons Terror and Panic, and daughter Clancy). The oldest and most inscrutable empire on the Discworld is in turmoil, brought about by the revolutionary treatise What I Did On My Holidays. Workers are uniting, with nothing to lose but their water buffaloes. Warlords are struggling for power. War (and Clancy) are spreading through the ancient cities. And all that stands in the way of terrible doom for everyone is: Rincewind the Wizzard, who can't even spell the word 'wizard' ... Cohen the barbarian hero, five foot tall in his surgical sandals, who has had a lifetime's experience of not dying ...and a very special butterfly.",
                  "bookCoverImage":null,
                  "bookCoverImageUrl":"http://wiki.lspace.org/mediawiki/images/9/96/Cover_Interesting_Times.jpg"
              }
            ]
       

1. Characters

   The `Characters` controller has two methods:

   1. All

   It is not recommended to use this action, it is included for testing purposes only. It is not recommended that users have access to this controller method as it will return ALL Character data in the system - which could be expensive both computationally and with refards to bandwidth (once the server is hosted).

    The `All` action takes no parameters and returns all Character records, ordered alphabetically by name. If the user wishes to request all data, they can do so by issuing the folllowing request:

        /Characters/All
    
    This will return a lot of data, a small sample of which is presented here as an example:

        [
          {
              "characterId":3,
              "characterName":"Ridcully"
          },
          {
              "characterId":1,
              "characterName":"Rincewind",
          },
          {
              "characterId":2,
              "characterName":"Two Flower",
          }
        ]

   1. Search

   The `Search` action takes a string parameter called `searchString`. `dwCheckApi` will search the names of all Character records, and return those which match.

   If the user wishes to search for the prase "flow", then they should issue the following request:

        /Characters/Search?searchString=flow
        
   This will return the following JSON data:

        [
          {
              "characterId":2,
              "characterName":"Two Flower",
          }
        ]

# Seeding the Database

During startup, in the Configure method, `dwCheck` will apply any outstanding mirgrations (which is not a fantastic practise, but will be ok for now) then seeds the database via the `EnsureSeedData` extention method. This is an automatic process and requires no user input.

# Data Source

The [L-Space wiki](http://wiki.lspace.org/mediawiki/Bibliography#Novels) is currently being used to seed the database. All data is copyright to Terry Pratchett and/or Transworld Publishers no infringement was intended.

# TODO

Strikethrough or remove when done

- Update README sections for models and returned data types