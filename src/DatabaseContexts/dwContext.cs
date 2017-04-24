using dwCheckApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace dwCheckApi.DatabaseContexts 
{
    public class DwContext : DbContext
    {
        public DwContext(DbContextOptions<DwContext> options) : base(options) { }
        public DwContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCharacter>().HasKey(x => new { x.BookId, x.CharacterId });
            modelBuilder.Entity<BookSeries>().HasKey(x => new { x.BookId, x.SeriesId });

            // Create shadow properties
            // For more information onShoadow properties, see:
            // https://docs.efproject.net/en/latest/modeling/shadow-properties.html
            // modelBuilder.Entity<BaseAuditClass>().Property<DateTime>("Modified");
            // modelBuilder.Entity<BaseAuditClass>().Property<DateTime>("Created");
        }

        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }

        private void ApplyAuditInformation()
        {
            var modifiedEntities = ChangeTracker.Entries<BaseAuditClass>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entity in modifiedEntities)
            {
                entity.Property("Modified").CurrentValue = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    entity.Property("Created").CurrentValue = DateTime.UtcNow;
                }
            }
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Series> Series {get; set;}
        public DbSet<BookCharacter> BookCharacters { get; set; }
        public DbSet<BookSeries> BookSeries { get; set; }
    }
}