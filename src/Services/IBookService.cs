using dwCheckApi.Models;
using System.Collections.Generic;

namespace dwCheckApi.Services 
{
    public interface IBookService
    {
        // Search and Get
        Book FindById(int id);
        Book FindByOrdinal (int id);
        Book GetByName(string bookName);
        IEnumerable<Book> Search(string searchKey);
    }
}