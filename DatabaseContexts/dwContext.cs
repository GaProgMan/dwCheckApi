using System;
using System.Linq;
using dwCheckApi.Models;

// the Entity Framework namespace
using Microsoft.EntityFrameworkCore;

namespace dwCheckApi.DatabaseContexts 
{
    public class DwContext : DbContext
    {
        public DwContext(DbContextOptions<DwContext> options) : base(options) { }
        public DwContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCharacter>()
                .HasOne(bc => bc.Book)
                .WithMany(book => book.BookCharacter)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCharacter>()
                .HasOne(bc => bc.Character)
                .WithMany(ch => ch.BookCharacter)
                .HasForeignKey(bc => bc.CharacterId);

            // Create shadow properties
            // For more information onShoadow properties, see:
            // https://docs.efproject.net/en/latest/modeling/shadow-properties.html
            modelBuilder.Entity<Book>().Property<DateTime>("Modified");
            modelBuilder.Entity<Book>().Property<DateTime>("Created");
        }

        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }

        private void ApplyAuditInformation()
        {
            // will only work for single entity type
            // TODO: create interface for all entity types
            var modifiedBooks = ChangeTracker
                .Entries<Book>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entry in modifiedBooks)
            {
                entry.Property("Modified").CurrentValue = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = DateTime.UtcNow;
                }
            }
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<BookCharacter> BookCharacter { get; set; }
    }
}