using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dwCheckApi.Entities
{
    public class Character : BaseAuditClass
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public virtual ICollection<BookCharacter> BookCharacter { get; set; } = new Collection<BookCharacter>();
    }
}