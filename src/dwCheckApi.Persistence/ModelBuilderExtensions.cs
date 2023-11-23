using System.Diagnostics.CodeAnalysis;
using dwCheckApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace dwCheckApi.Persistence
{
    [ExcludeFromCodeCoverage]
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Used to create the the primary keys for dwCheckApi's database model
        /// </summary>
        /// <param name="builder">An instance of the <see cref="ModelBuilder"/> to act on</param>
        public static void AddPrimaryKeys(this ModelBuilder builder)
        {
            builder.Entity<Book>().ToTable("Books")
                .HasKey(b => b.BookId);
            
            builder.Entity<Character>().ToTable("Characters")
                .HasKey(c => c.CharacterId);
            
            builder.Entity<Series>().ToTable("Series")
                .HasKey(s => s.SeriesId);
            
            builder.Entity<BookCharacter>().ToTable("BookCharacters")
                .HasKey(x => new {x.BookId, x.CharacterId});
            
            builder.Entity<BookSeries>().ToTable("BookSeries")
                .HasKey(x => new { x.BookId, x.SeriesId });
        }
    }
}