using dwCheckApi.Models;
using System.Collections.Generic;

namespace dwCheckApi.Services 
{
    public interface ICharacterService
    {
                // Search and Get
        Character FindById(int id);
        IEnumerable<Character> Search(string searchKey);
        IEnumerable<Character> GetAll();
    }
}