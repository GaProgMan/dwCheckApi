using System;
using System.Collections.Generic;
using System.Linq;
using dwCheckApi.DTO.Helpers;
using dwCheckApi.DTO.ViewModels;
using dwCheckApi.Entities;
using Xunit;

namespace dwCheckApi.Tests.ViewModelMappers
{
    public class BookViewModelMapperTests
    {
        [Fact]
        public void Given_BookDbModel_Returns_ViewModel()
        {
            // Arrange
            const int idForTest = 1;
            var dbBook = GetTestBookById(idForTest);
            var testViewModel = GetBookViewModels()
                    .FirstOrDefault(b => b.BookOrdinal == idForTest);
            // Act
            var viewModel = BookViewModelHelpers.ConvertToViewModel(dbBook);
            
            // Assert
            Assert.NotNull(testViewModel);
            Assert.Equal(testViewModel.BookName, viewModel.BookName);
            Assert.Equal(testViewModel.BookDescription, viewModel.BookDescription);
            Assert.Equal(testViewModel.BookIsbn10, viewModel.BookIsbn10);
            Assert.Equal(testViewModel.BookIsbn13, viewModel.BookIsbn13);
        }
        
        [Fact]
        public void Given_BookDbModels_Returns_ViewModels()
        {
            // Arrange
            const int idForTest = 1;
            var dbBooks = GetTestBooks();
            // Act
            var viewModels = BookViewModelHelpers.ConvertToViewModels(dbBooks);
            
            // Assert
            Assert.NotEmpty(viewModels);
            Assert.Equal(viewModels.Count, dbBooks.Count);
            
            for (var i = 0; i < viewModels.Count; i++)
            {
                Assert.Equal(viewModels[i].BookName, dbBooks[i].BookName);
                Assert.Equal(viewModels[i].BookDescription, dbBooks[i].BookDescription);
                Assert.Equal(viewModels[i].BookIsbn10, dbBooks[i].BookIsbn10);
                Assert.Equal(viewModels[i].BookIsbn13, dbBooks[i].BookIsbn13);   
            }
        }
        
        [Fact]
        public void Given_BookDbModels_Returns_BaseViewModels()
        {
            // Arrange
            const int idForTest = 1;
            var dbBooks = GetTestBooks();
            // Act
            var viewModels = BookViewModelHelpers.ConvertToBaseViewModels(dbBooks);
            
            // Assert
            Assert.NotEmpty(viewModels);
            Assert.Equal(viewModels.Count, dbBooks.Count);
            
            for (var i = 0; i < viewModels.Count; i++)
            {
                Assert.Equal(viewModels[i].BookId, dbBooks[i].BookId);
                Assert.Equal(viewModels[i].BookOrdinal, dbBooks[i].BookOrdinal);
                Assert.Equal(viewModels[i].BookName, dbBooks[i].BookName);
                Assert.Equal(viewModels[i].BookDescription, dbBooks[i].BookDescription);
            }
        }
        
        [Fact]
        public void Given_BookDbModel_Returns_BookCoverViewModel()
        {
            // Arrange
            const int idForTest = 1;
            var dbBook = GetTestBookById(idForTest);
            // Act
            var viewModel = BookViewModelHelpers.ConvertToBookCoverViewModel(dbBook);
            
            // Assert
            Assert.NotNull(viewModel);
            Assert.Equal(viewModel.bookId, dbBook.BookId);
            Assert.Equal(viewModel.BookCoverImage, dbBook.BookCoverImageUrl);
            Assert.False(viewModel.BookImageIsBase64String);
        }

        private Book GetTestBookById(int id)
        {
            return GetTestBooks().FirstOrDefault(b => b.BookId == id);
        }
        
        private List<Book> GetTestBooks()
        {
            var testSeries = new Series
            {
                SeriesName = "A test series",
                SeriesId = 2
            };

            var testCharacter = new Character
            {
                CharacterName = Guid.NewGuid().ToString(),
                CharacterId = 4
            };
            
            var mockData = new List<Book>();
            mockData.Add(new Book
            {
                BookId = 1,
                BookName = "Test Book",
                BookOrdinal = 1,
                BookDescription = "Test entry for unit tests only",
                BookIsbn10 = "1234567890",
                BookIsbn13 = "1234567890123",
                BookCoverImage = new List<byte>().ToArray(),
                BookSeries = new List<BookSeries>
                {
                    new()
                    {
                        BookId = 1,
                        SeriesId = testSeries.SeriesId,
                        Series = testSeries,
                        Ordinal = 3
                    }
                },
                BookCharacter = new List<BookCharacter>
                {
                    new()
                    {
                        BookId = 1,
                        CharacterId = testCharacter.CharacterId,
                        Character = testCharacter
                    }
                }
            });

            return mockData;
        }

        private List<BookViewModel> GetBookViewModels()
        {
            var viewModels = new List<BookViewModel>();
            viewModels.Add(new BookViewModel
            {
                BookOrdinal = 1,
                BookName = "Test Book",
                BookDescription = "Test entry for unit tests only",
                BookIsbn10 = "1234567890",
                BookIsbn13 = "1234567890123"
            });

            return viewModels;
        }
    }
}