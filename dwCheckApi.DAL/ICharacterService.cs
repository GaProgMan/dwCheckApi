using System.Collections.Generic;
using dwCheckApi.Entities;

namespace dwCheckApi.DAL 
{
    public interface ICharacterService
    {
        Character GetById (int id);
        Character GetByName (string characterName);
        IEnumerable<Character> Search(string searchKey);
    }
}