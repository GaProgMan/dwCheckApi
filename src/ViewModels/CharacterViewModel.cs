using System.Collections.Generic;

namespace dwCheckApi.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        public CharacterViewModel()
        { 
            Books = new List<string>();
        }

        public string CharacterName { get; set; }
        public List<string> Books { get; set; }
    }
}