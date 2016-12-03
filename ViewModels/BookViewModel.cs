using System.Collections.Generic;

namespace dwCheckApi.ViewModels
{
    public class BookViewModel
    {
        public BookViewModel()
        {
            Characters = new List<string>();
        }

        public int BookOrdinal { get; set; }
        public string BookName { get; set; }
        public string BookIsbn10 { get; set; }
        public string BookIsbn13 { get; set; }
        public string BookDescription { get; set; }
        public byte[] BookCoverImage { get; set; }
        public string BookCoverImageUrl { get; set; }
        public List<string> Characters { get; set; }
    }
}