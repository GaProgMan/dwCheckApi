using dwCheckApi.Models;
using dwCheckApi.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace dwCheckApi.Helpers
{
    public static class CharacterViewModelHelpers
    {
        public static CharacterViewModel ConvertToviewModel (Character dbModel)
        {
            var viewModel = new CharacterViewModel
            {
                CharacterName = dbModel.CharacterName
            };

            foreach (var bc in dbModel.BookCharacter)
            {
                viewModel.Books.Add(bc.Book.BookName ?? string.Empty);
            }

            return viewModel;
        }

        public static List<CharacterViewModel> ConvertToViewModels(List<Character> dbModels)
        {
            return dbModels.Select(ch => ConvertToviewModel(ch)).ToList();
        }
    }
}