using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dwCheckApi.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public virtual ICollection<BookCharacter> BookCharacter { get; set; } = new Collection<BookCharacter>();
    }
}