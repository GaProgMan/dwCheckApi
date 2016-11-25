using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace dwCheckApi.Models
{
    public class BookCharacter
    {
        public int BookCharacterId { get; set; }
        public int BookId { get; set; }
        public int CharacterId {get; set; }
        public virtual Book Book { get; set; }
        public virtual Character Character { get; set; }
    }
}