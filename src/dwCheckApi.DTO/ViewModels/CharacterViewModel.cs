using System.Collections.Generic;

namespace dwCheckApi.DTO.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        public CharacterViewModel()
        { 
            Books = new SortedDictionary<int, string>();
        }

        public string CharacterName { get; set; }
        public SortedDictionary<int, string> Books { get; set; }
    }
}