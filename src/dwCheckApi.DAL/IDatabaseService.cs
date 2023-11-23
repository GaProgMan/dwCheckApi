using System.Collections.Generic;
using System.Threading.Tasks;
using dwCheckApi.Entities;

namespace dwCheckApi.DAL 
{
    public interface IDatabaseService
    {
        bool ClearDatabase();

        int SeedDatabase();

        IEnumerable<Book> BooksWithoutCoverBytes();

        Task<int> SaveAnyChanges();
    }
}