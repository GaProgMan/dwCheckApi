using dwCheckApi.DatabaseTools;
using dwCheckApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace dwCheckApi.DatabaseContexts
{
    // For a description of this file, see the "Seeding" section of:
    // https://blogs.msdn.microsoft.com/dotnet/2016/09/29/implementing-seeding-custom-conventions-and-interceptors-in-ef-core-1-0/
    public static class DatabaseContextExtentsions
    {
        public static bool AllMigrationsApplied(this DwContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeedData(this DwContext context)
        {
            if (context.AllMigrationsApplied())
            {
                var dbSeeder = new DatabaseSeeder(context);
                if (!context.Books.Any())
                {
                    var pathToSeedData = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookSeedData.json");;
                    dbSeeder.SeedBookEntitiesFromJson(pathToSeedData);
                }
                if (!context.BookCharacters.Any())
                {
                    dbSeeder.SeedBookCharacterEntriesFromJson();
                }
                if (!context.BookSeries.Any())
                {
                    dbSeeder.SeedBookSeriesEntriesFromJson();
                }

                context.SaveChanges();
            }
        }
    }
}