using System.Threading;
using System.Threading.Tasks;
using dwCheckApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace dwCheckApi.Persistence
{
    public interface IDwContext
    {
        /// <summary>
        /// Asynchronously saves all changes made in the DwContext to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        ///     Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        ///     been sent successfully to the database.
        /// </param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess = true,
            CancellationToken cancellationToken = default(CancellationToken));

        DbSet<Book> Books { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<Series> Series { get; set; }
        DbSet<BookCharacter> BookCharacters { get; set; }
        DbSet<BookSeries> BookSeries { get; set; }
    }
}