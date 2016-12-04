using dwCheckApi.Models;
using System.Collections.Generic;

namespace dwCheckApi.Services 
{
    public interface ICharacterService
    {
        Character GetById (int id);
        Character GetByName (string characterName);
        IEnumerable<Character> Search(string searchKey);
    }
}