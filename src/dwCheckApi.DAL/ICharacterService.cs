using System.Collections.Generic;
using System.Linq;
using dwCheckApi.Entities;

namespace dwCheckApi.DAL 
{
    public interface ICharacterService
    {
        Character GetById (int id);
        Character GetByName (string characterName);
        IEnumerable<IGrouping<string, BookCharacter>> Search(string searchKey);
    }
}