namespace dwCheckApi.DTO.ViewModels
{
    public class BookCoverViewModel : BaseViewModel
    {
        public int bookId { get; set; }
        public string BookCoverImage { get; set; }
        public bool BookImageIsBase64String { get; set; }
    }
}