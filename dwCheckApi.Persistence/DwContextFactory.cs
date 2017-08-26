using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace dwCheckApi.Persistence
{
    /// <summary>
    /// This factory is provided so that the EF Core tools can build a full context
    /// without having to have access to where the DbContext is being created (i.e.
    /// in the UI layer).
    /// </summary>
    /// <remarks>
    /// Please see the following URL for more information:
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext#using-idbcontextfactorytcontext
    /// </remarks>
    public class DwContextFactory : IDesignTimeDbContextFactory<DwContext>
    {
        public DwContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DwContext>();
            
            // TODO: don't hard code this
            optionsBuilder.UseSqlite("Data Source=dwDatabase.db");
            
            return new DwContext(optionsBuilder.Options);
        }
    }
}