using dwCheckApi.DTO.ViewModels;
using dwCheckApi.Entities;

namespace dwCheckApi.DTO.Helpers
{
    public static class BookCharacterViewModelHelpers
    {
        public static BookCharacterViewModel ConvertToViewModel(BookCharacter dbModel)
        {
            return new BookCharacterViewModel
            {
                Book = BookViewModelHelpers.ConvertToViewModel(dbModel.Book),
                Character = CharacterViewModelHelpers.ConvertToViewModel(dbModel.Character)
            };
        }
    }
}