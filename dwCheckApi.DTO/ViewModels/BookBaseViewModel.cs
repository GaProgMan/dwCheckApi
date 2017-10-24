namespace dwCheckApi.DTO.ViewModels
{
    public class BookBaseViewModel : BaseViewModel
    {
        public string BookDescription { get; set; }
        public string BookCoverImage { get; set; }
        public bool BookImageIsBase64String { get; set; }
    }
}