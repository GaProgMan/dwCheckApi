using System;
using System.Reflection;

namespace dwCheckApi.Helpers
{
    public static class CommonHelpers
    {
        /// Hard code this list for now, as .NET Core 1.0
        /// doesn't have reflection
        public static string IncorrectUsageOfApi()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append($"Incorrect useage of API{Environment.NewLine}");

            sb.Append($"The following functions are available for Books:{Environment.NewLine}");
            sb.Append($"\t'/Books/GetByOrdinal' - Returns a single book, by it's ordinal (release order){Environment.NewLine}");
            sb.Append($"\t'/Books/GetByName - Returns a single book whose name matches the name passed in (?bookName=) {Environment.NewLine}");
            sb.Append($"\t'/Books/Search' - Searches all Books for a search string (?searchString=){Environment.NewLine}");

            sb.Append($"The following functions are available for Characters:{Environment.NewLine}");
            sb.Append($"\t'/Characters/Get' - Returns a single character by it's ID (set in the database){Environment.NewLine}");
            sb.Append($"\t'/Characters/GetByName' - Returns a single Character my their name (?characterName=), must match exactly{Environment.NewLine}");
            sb.Append($"\t'/Characters/Search' - Searches all Characters for a search string (?searchString=){Environment.NewLine}");

            sb.Append($"The following functions are available for Series:{Environment.NewLine}");
            sb.Append($"\t'/Series/Get' - Returns a single Series by it's ID (set in the database){Environment.NewLine}");
            sb.Append($"\t'/Series/GetByName' - Returns a single Series my it's name (?seriesName=), must match exactly{Environment.NewLine}");
            sb.Append($"\t'/Series/Search' - Searches all Series for a search string (?searchString=){Environment.NewLine}");

            sb.Append($"The following functions are available for the Database itself:{Environment.NewLine}");
            sb.Append($"\t'/Database/ApplyBookCoverArt' - Looks through the database for books without cover art and gets the Base64 string which represents the book cover art{Environment.NewLine}");
            sb.Append($"\t'/Database/DropData' - Useful for dropping all data from the database{Environment.NewLine}");
            sb.Append($"\t'/Database/SeedData - Useful for seeding all data (read from a series of JSON files){Environment.NewLine}");
            
            sb.Append($"The following functions are available for the application itself:{Environment.NewLine}");
            sb.Append($"\t'/Version - Returns the semver fromatted version string for this application{Environment.NewLine}");
            sb.Append($"\t'/swagger - Returns Swagger formatted API documentation for the application{Environment.NewLine}");
            
            return sb.ToString();
        }

        public static string GetVersionNumber()
        {
            return Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;
        }
    }
}