using System.Collections.Generic;
using System.Linq;
using dwCheckApi.DTO.ViewModels;
using dwCheckApi.Entities;

namespace dwCheckApi.DTO.Helpers
{
    public static class BookViewModelHelpers
    {
        public static BookViewModel ConvertToViewModel (Book dbModel)
        {
            var viewModel = new BookViewModel
            {
                BookId = dbModel.BookId,
                BookOrdinal = dbModel.BookOrdinal,
                BookName = dbModel.BookName,
                BookIsbn10 = dbModel.BookIsbn10,
                BookIsbn13 = dbModel.BookIsbn13,
                BookDescription = dbModel.BookDescription,
                BookCoverImage = dbModel.BookCoverImage,
                BookCoverImageUrl = dbModel.BookCoverImageUrl
            };

            foreach (var bc in dbModel.BookCharacter)
            {
                viewModel.Characters.Add(bc.Character.CharacterName ?? string.Empty);
            }

            foreach(var series in dbModel.BookSeries)
            {
                viewModel.Series.Add(series.Series.SeriesName ?? string.Empty);
            }

            return viewModel;
        }

        public static List<BookViewModel> ConvertToViewModels(List<Book> dbModel)
        {
            return dbModel.Select(book => ConvertToViewModel(book)).ToList();
        }

        public static BookBaseViewModel ConvertToBaseViewModel(Book dbModel)
        {
            var viewModel = new BookBaseViewModel
            {
                BookCoverImageUrl = dbModel.BookCoverImageUrl,
                BookDescription = dbModel.BookDescription
            };
            

            return viewModel;
        }

        public static List<BookBaseViewModel> ConvertToBaseViewModels(List<Book> dbModel)
        {
            return dbModel.Select(book => ConvertToBaseViewModel(book)).ToList();
        }

    }
}