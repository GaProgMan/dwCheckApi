using dwCheckApi.Models;
using dwCheckApi.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace dwCheckApi.Services 
{
    public class BookService : IBookService
    {
        private DwContext _dwContext;

        public BookService (DwContext dwContext)
        {
            _dwContext = dwContext;
        }

        public Book FindById(int id)
        {
            return BaseQuery()
                .FirstOrDefault(book => book.BookId == id);
        }

        public Book FindByOrdinal (int id)
        {
            return BaseQuery()
                .FirstOrDefault(book => book.BookOrdinal == id);
        }

        public IEnumerable<Book> Search(string searchKey)
        {
            var blankSearchString = string.IsNullOrWhiteSpace(searchKey);

            var results = BaseQuery();

            if (!blankSearchString)
            {
                searchKey = searchKey.ToLower();
                BaseQuery()
                    .Where(book => book.BookName.ToLower().Contains(searchKey)
                        || book.BookDescription.ToLower().Contains(searchKey)
                        || book.BookIsbn10.ToLower().Contains(searchKey)
                        || book.BookIsbn13.ToLower().Contains(searchKey));
            }
                

            return results.OrderBy(book => book.BookOrdinal);
        }

        private IEnumerable<Book> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _dwContext.Books
                .AsNoTracking()
                .Include(book => book.BookCharacter)
                .ThenInclude(bookCharacter => bookCharacter.Character);
        }
    }
}
