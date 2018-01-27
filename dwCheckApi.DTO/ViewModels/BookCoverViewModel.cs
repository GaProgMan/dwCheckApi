namespace dwCheckApi.DTO.ViewModels
{
    public class BookCoverViewModel : BaseViewModel
    {
        public string BookCoverImage { get; set; }
        public bool BookImageIsBase64String { get; set; }
    }
}