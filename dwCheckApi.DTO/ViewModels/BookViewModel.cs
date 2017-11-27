using System.Collections.Generic;

namespace dwCheckApi.DTO.ViewModels
{
    public class BookViewModel : BookBaseViewModel
    {
        public BookViewModel()
        {
            Characters = new List<string>();
            Series = new List<string>();
        }

        public int BookId { get; set; }
        public int BookOrdinal { get; set; }
        public string BookName { get; set; }
        public string BookIsbn10 { get; set; }
        public string BookIsbn13 { get; set; }
        public List<string> Characters { get; set; }
        public List<string> Series { get; set; }
    }
}