using System.Collections.Generic;

namespace dwCheckApi.DTO.ViewModels
{
    public class CharacterViewModel : BaseViewModel
    {
        public string CharacterName { get; set; }
        public SortedDictionary<int, string> Books { get; set; } = new();
    }
}