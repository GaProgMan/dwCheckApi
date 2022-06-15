using System.Collections.Generic;
using System.Linq;
using dwCheckApi.DTO.Helpers;
using dwCheckApi.DTO.ViewModels;
using dwCheckApi.Entities;
using Xunit;
using Xunit.Abstractions;

namespace dwCheckApi.Tests.ViewModelMappers
{
    public class BookViewModelMapperTests
    {
        public BookViewModelMapperTests(ITestOutputHelper output)
        {
        }
        
        [Fact]
        public void Given_BookDbModel_Returns_ViewModel()
        {
            // What is this test even testing?
            
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

        private Book GetTestBookById(int id)
        {
            return GetTestBooks().FirstOrDefault(b => b.BookId == id);
        }
        
        private List<Book> GetTestBooks()
        {
            var mockData = new List<Book>();
            mockData.Add(new Book
            {
                BookId = 1,
                BookName = "Test Book",
                BookOrdinal = 1,
                BookDescription = "Test entry for unit tests only",
                BookIsbn10 = "1234567890",
                BookIsbn13 = "1234567890123",
                BookCoverImage = new List<byte>().ToArray()
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