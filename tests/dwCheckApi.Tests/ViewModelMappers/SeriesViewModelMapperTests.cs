using System;
using System.Collections.Generic;
using System.Linq;
using dwCheckApi.DTO.Helpers;
using dwCheckApi.Entities;
using Xunit;

namespace dwCheckApi.Tests.ViewModelMappers
{
    public class SeriesViewModelMapperTests
    {
        [Fact]
        public void Given_SeriesDbModel_Returns_ViewModel()
        {
            // Arrange
            var dbBook = new Book
            {
                BookName = Guid.NewGuid().ToString()
            };
            var dbSeries = new Series
            {
                SeriesId = 1,
                SeriesName = Guid.NewGuid().ToString(),
                BookSeries = new List<BookSeries>
                {
                    new()
                    {
                        Book = dbBook
                    }
                }
            };

            // Act
            var viewModel = SeriesViewModelHelpers.ConvertToViewModel(dbSeries);

            // Assert
            Assert.Equal(dbSeries.SeriesId, viewModel.SeriesId);
            Assert.Equal(dbSeries.SeriesName, viewModel.SeriesName);
            Assert.Equal(dbSeries.BookSeries.First().Book.BookName, viewModel.BookNames.First());
        }
        
        [Fact]
        public void Given_ListOfSeriesDbModel_Returns_ListOfViewModel()
        {
            // Arrange
            var dbSeries = new List<Series>
            {
                new()
                {
                    SeriesId = 1,
                    SeriesName = Guid.NewGuid().ToString(),
                    BookSeries = new List<BookSeries>()
                }
            };

            // Act
            var viewModels = SeriesViewModelHelpers.ConvertToViewModels(dbSeries);

            // Assert
            Assert.NotNull(viewModels);
            Assert.NotEmpty(viewModels);
            Assert.Equal(dbSeries.Count, viewModels.Count);
            
            for (var i = 0; i < viewModels.Count; i++)
            {
                Assert.Equal(dbSeries[i].SeriesId, viewModels[i].SeriesId);
                Assert.Equal(dbSeries[i].SeriesName, viewModels[i].SeriesName);
            }
        }
    }
}