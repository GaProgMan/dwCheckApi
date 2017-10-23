using System.Collections.Generic;
using System.Linq;
using dwCheckApi.DTO.ViewModels;
using dwCheckApi.Entities;

namespace dwCheckApi.DTO.Helpers
{
    public static class SeriesViewModelHelpers
    {
        public static SeriesViewModel ConvertToViewModel (Series dbModel)
        {
            var viewModel = new SeriesViewModel
            {
                SeriesId = dbModel.SeriesId,
                SeriesName = dbModel.SeriesName
            };

            foreach(var dbBook in dbModel.BookSeries.OrderBy(bs => bs.Ordinal))
            {
                viewModel.BookNames.Add(dbBook.Book.BookName ?? string.Empty);
            }

            return viewModel;
        }

        public static List<SeriesViewModel> ConvertToViewModels(List<Series> dbModels)
        {
            return dbModels.Select (s => ConvertToViewModel(s)).ToList();
        }
    }
}