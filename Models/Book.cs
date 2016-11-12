using System;

namespace dwCheckApi.Models
{
    public class Book
    {
        // our primary key
        public int BookId { get; set; }
        public int BookOrdinal { get; set; }
        public string BookName { get; set; }
        public string BookIsbn10 { get; set; }
        public string BookIsbn13 { get; set; }
        public string BookDescription { get; set; }
        public byte[] BookCoverImage { get; set; }
        public string BookCoverImageUrl { get; set; }
    }
}