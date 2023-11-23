using System.Collections.Generic;
using dwCheckApi.DTO.ViewModels;

namespace dwCheckApi.DTO.Helpers
{
    public static class CharacterViewModelHelpers
    {
        public static CharacterViewModel ConvertToViewModel (string characterName, Dictionary<int, string> books = null)
        {
            var viewModel = new CharacterViewModel
            {
                CharacterName = characterName
            };

            if (books != null)
            {
                viewModel.Books = new SortedDictionary<int, string>(books);
            }

            return viewModel;
        }
    }
}