using dwCheckApi.Models;
using dwCheckApi.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dwCheckApi.Services 
{
    public class CharacterService : ICharacterService
    {
        private DwContext _dwContext;

        public CharacterService (DwContext dwContext)
        {
            _dwContext = dwContext;
        }

        public IEnumerable<Character> GetAll()
        {
            return _dwContext.Characters.AsNoTracking();
        }

        public IEnumerable<Character> Search(string searchKey)
        {
            return _dwContext
                .Characters
                .AsTracking()
                .Where(ch => ch.CharacterName.Contains(searchKey));

        }
    }
}