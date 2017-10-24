using System.Collections.Generic;
using dwCheckApi.Entities;

namespace dwCheckApi.DAL 
{
    public interface IBookService
    {
        // Search and Get
        Book FindById(int id);
        Book FindByOrdinal (int id);
        Book GetByName(string bookName);
        IEnumerable<Book> Search(string searchKey);
        IEnumerable<Book> Series(int seriesId);
    }
}