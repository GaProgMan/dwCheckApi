using dwCheckApi.Models;
using dwCheckApi.DatabaseContexts;

using Microsoft.EntityFrameworkCore;

using System;
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

        public void AddOrUpdate(Book item)
        {
            throw new NotImplementedException();
        }

        public Book FindById(int id)
        {
            return _dwContext.Books
                   .FirstOrDefault(book => book.BookId == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _dwContext.Books.AsNoTracking();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> Search(string searchKey)
        {
            return _dwContext.Books
                       .AsNoTracking()
                       .Where(book => book.BookName.Contains(searchKey)
                           || book.BookDescription.Contains(searchKey)
                           || book.BookIsbn10.Contains(searchKey)
                           || book.BookIsbn13.Contains(searchKey));
        }

        public void Update(Book item)
        {
            throw new NotImplementedException();
        }
    }
}