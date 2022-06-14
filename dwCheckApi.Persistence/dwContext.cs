using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using dwCheckApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace dwCheckApi.Persistence
{
    [ExcludeFromCodeCoverage]
    public class DwContext : DbContext, IDwContext
    {
        public DwContext(DbContextOptions<DwContext> options) : base(options) { }
        public DwContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddPrimaryKeys();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.ApplyAuditInformation();
            
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Series> Series {get; set;}
        public DbSet<BookCharacter> BookCharacters { get; set; }
        public DbSet<BookSeries> BookSeries { get; set; }
    }
}