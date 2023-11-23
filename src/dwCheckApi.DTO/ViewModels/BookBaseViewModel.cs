namespace dwCheckApi.DTO.ViewModels
{
    public class BookBaseViewModel : BaseViewModel
    {
        public int BookId { get; set; }
        public int BookOrdinal { get; set; }
        public string BookName { get; set; }
        public string BookDescription { get; set; }
    }
}