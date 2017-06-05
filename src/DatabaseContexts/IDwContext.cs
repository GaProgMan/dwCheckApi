using dwCheckApi.Models;
using Microsoft.EntityFrameworkCore;

namespace dwCheckApi.DatabaseContexts
{
    public interface IDwContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<Series> Series {get; set;}
        DbSet<BookCharacter> BookCharacters { get; set; }
        DbSet<BookSeries> BookSeries { get; set; }

        int SaveChanges();
    }
}