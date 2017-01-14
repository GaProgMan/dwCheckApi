using dwCheckApi.Models;
using dwCheckApi.ViewModels;

using System.Collections.Generic;
using System.Linq;

namespace dwCheckApi.Helpers
{
    public static class BookCharacterViewModelHelpers
    {
        public static BookCharacterViewModel ConvertToViewModel(BookCharacter dbModel)
        {
            return new BookCharacterViewModel
            {
                Book = BookViewModelHelpers.ConvertToViewModel(dbModel.Book),
                Character = CharacterViewModelHelpers.ConvertToviewModel(dbModel.Character)
            };
        }
    }
}