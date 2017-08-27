using System.IO;
using System.Linq;
using dwCheckApi.Persistence.Helpers;

namespace dwCheckApi.Persistence
{
    public static class DwContextExtensions
    {
        public static int EnsureSeedData(this DwContext context)
        {
            var bookCount = default(int);
            var characterCount = default(int);
            var bookSeriesCount = default(int);

            // Because each of the following seed method needs to do a save
            // (the data they're importing is relational), we need to call
            // SaveAsync within each method.
            // So let's keep tabs on the counts as they come back

            var dbSeeder = new DatabaseSeeder(context);
            if (!context.Books.Any())
            {
                var pathToSeedData = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookSeedData.json");
                bookCount = dbSeeder.SeedBookEntitiesFromJson(pathToSeedData).Result;
            }
            if (!context.BookCharacters.Any())
            {
                characterCount = dbSeeder.SeedBookCharacterEntriesFromJson().Result;
            }
            if (!context.BookSeries.Any())
            {
                bookSeriesCount = dbSeeder.SeedBookSeriesEntriesFromJson().Result;
            }

            return bookCount + characterCount + bookSeriesCount;
        }
    }
}