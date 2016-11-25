using dwCheckApi.Models;
using System.Collections.Generic;

namespace dwCheckApi.Services 
{
    public interface ICharacterService
    {
        // Search and Get
        IEnumerable<Character> Search(string searchKey);
        Character GetById (int id);
    }
}