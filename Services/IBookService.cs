using dwCheckApi.Models;
using System.Collections.Generic;

namespace dwCheckApi.Services 
{
    public interface IBookService
    {
        // Search and Get
        Book FindById(int id);
        Book FindByOrdinal (int id);
        IEnumerable<Book> Search(string searchKey);
        IEnumerable<Book> GetAll();
    }
}
