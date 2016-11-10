using dwCheckApi.Models;
using dwCheckApi.Services;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

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
                if(!context.Books.Any())
                {
                    context.Books.Add(new Book{
                        BookName = "The Colour of Magic",
                        BookIsbn10 = "086140324X",
                        BookIsbn13 = "9780552138932",
                        BookDescription = "On a world supported on the back of a giant turtle (sex unknown), a gleeful, explosive, wickedly eccentric expedition sets out. There's an avaricious but inept wizard, a naive tourist whose luggage moves on hundreds of dear little legs, dragons who only exist if you believe in them, and of course THE EDGE of the planet ...",
                        BookCoverImageUrl = "http://wiki.lspace.org/mediawiki/images/c/c9/Cover_The_Colour_Of_Magic.jpg"
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}