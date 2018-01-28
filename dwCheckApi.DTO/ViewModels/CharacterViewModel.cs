using System.Collections.Generic;

namespace dwCheckApi.DTO.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        public CharacterViewModel()
        { 
            Books = new Dictionary<int, string>();
        }

        public string CharacterName { get; set; }
        public Dictionary<int, string> Books { get; set; }
    }
}