using System;
using dwCheckApi.Models;

// the Entity Framework namespace
using Microsoft.EntityFrameworkCore;

namespace dwCheckApi.DatabaseContexts 
{
    public class DwContext : DbContext
    {
        public DwContext(DbContextOptions<DwContext> options) : base(options) { }
        public DwContext() { }

        public DbSet<Book> Books { get; set; }
    }
}