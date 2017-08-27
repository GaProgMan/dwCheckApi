using System.Collections.Generic;
using System.Linq;
using dwCheckApi.Entities;
using dwCheckApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace dwCheckApi.DAL
{
    public class CharacterService : ICharacterService
    {
        private readonly DwContext _dwContext;

        public CharacterService (DwContext dwContext)
        {
            _dwContext = dwContext;
        }

        public IEnumerable<Character> Search(string searchKey)
        {
            var blankSearchString = string.IsNullOrEmpty(searchKey);

            var results = BaseQuery();

            if (!blankSearchString)
            {
                searchKey = searchKey.ToLower();
                results = BaseQuery()
                    .Where(ch => ch.CharacterName.ToLower().Contains(searchKey));
            }

            return results.OrderBy(ch => ch.CharacterName);
        }

        public Character GetById (int id)
        {
            return BaseQuery()
                .FirstOrDefault(character => character.CharacterId == id);
        }

        public Character GetByName(string characterName)
        {
            if(string.IsNullOrWhiteSpace(characterName))
            {
                // TODO : what here?
                return null;
            }

            characterName = characterName.ToLower();

            return BaseQuery().FirstOrDefault(ch => ch.CharacterName.ToLower() == characterName);
        }

        private IEnumerable<Character> BaseQuery()
        {
            // Explicit joins of entities is taken from here:
            // https://weblogs.asp.net/jeff/ef7-rc-navigation-properties-and-lazy-loading
            // At the time of committing 5da65e093a64d7165178ef47d5c21e8eeb9ae1fc, Entity
            // Framework Core had no built in support for Lazy Loading, so the above was
            // used on all DbSet queries.
            return _dwContext.Characters
                .AsNoTracking()
                .Include(character => character.BookCharacter)
                .ThenInclude(bookCharacter => bookCharacter.Book);
        }
    }
}