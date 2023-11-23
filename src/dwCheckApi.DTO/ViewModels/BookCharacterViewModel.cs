namespace dwCheckApi.DTO.ViewModels
{
    public class BookCharacterViewModel : BaseViewModel
    {
        public BookViewModel Book {get; set;}
        public CharacterViewModel Character {get; set;}
    }
}