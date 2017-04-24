using System.Collections.Generic;

namespace dwCheckApi.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        public BookViewModel()
        {
            Characters = new List<string>();
            Series = new List<string>();
        }

        public int BookOrdinal { get; set; }
        public string BookName { get; set; }
        public string BookIsbn10 { get; set; }
        public string BookIsbn13 { get; set; }
        public string BookDescription { get; set; }
        public byte[] BookCoverImage { get; set; }
        public string BookCoverImageUrl { get; set; }
        public List<string> Characters { get; set; }
        public List<string> Series { get; set; }
    }
}