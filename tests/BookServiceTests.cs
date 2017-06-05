using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using dwCheckApi.DatabaseContexts;
using dwCheckApi.DatabaseTools;
using dwCheckApi.Helpers;
using dwCheckApi.Models;
using dwCheckApi.ViewModels;
using Moq;
using Xunit;
using System.IO;
using System;
using System.Diagnostics;
using Xunit.Abstractions;

namespace dwCheckApi.Tests
{
    public class BookServiceTests
    {
        private readonly ITestOutputHelper output;

         public BookServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Fact]
        public void BookService_GetBookByOrdinal_ReturnsOneBook()
        {
            // Arrange
            var idForTest = 1;
            var dbBook = GetTestBookById(idForTest);
            var testViewModel = GetBookViewModels()
                    .FirstOrDefault(b => b.BookOrdinal == idForTest);
            // Act
            var viewModel = BookViewModelHelpers.ConvertToViewModel(dbBook);
            
            // Assert
            Assert.Equal(testViewModel.BookName, viewModel.BookName);
            Assert.Equal(testViewModel.BookDescription, viewModel.BookDescription);
            Assert.Equal(testViewModel.BookIsbn10, viewModel.BookIsbn10);
            Assert.Equal(testViewModel.BookIsbn13, viewModel.BookIsbn13);
        }

        // [Fact]
        // public void DbSeeder_SeedBookData_NoDataSupplied()
        // {
        //     // Arrange
        //     var mockBookSet = new Mock<DbSet<Book>>();
        //     var mockset = new Mock<DwContext>();
        //     mockset.Setup(m => m.Books).Returns(mockBookSet.Object);

        //     // Act & Assert
        //     var dbSeeder = new DatabaseSeeder(mockset.Object);
        //     ArgumentException argEx = Assert.Throws<ArgumentException>(() =>
        //         dbSeeder.SeedBookEntitiesFromJson(string.Empty));
        //     Assert.Contains("must be supplied to", argEx.Message);
        // }

        // [Fact]
        // public void DbSeeder_SeedBookData_DataSupplied()
        // {
        //     // Arrange
        //     // TODO Add an interface here, to mock stuff properly
        //     var bookList = new List<Book>();
        //     var mockBookSet = GetQueryableDbSet(bookList);
        //     var mockset = new Mock<IDwContext>();
        //     mockset.Setup(m => m.Books).Returns(mockBookSet.Object);
        //     var testJsonDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedData");
        //     if (!Directory.Exists(testJsonDirectory))
        //     {
        //         output.WriteLine("SeedData directory does not exist!");
        //     }
        //     if (File.Exists(Path.Combine(testJsonDirectory, "TestBookSeedData.json")))
        //     {
        //         foreach(var file in Directory.GetFiles(testJsonDirectory))
        //         {
        //             output.WriteLine(file);
        //         }
        //     }
        //     var pathToSeedData = Path.Combine(testJsonDirectory, "TestBookSeedData.json");
        //     var dbSeeder = new DatabaseSeeder(mockset.Object);
            
        //     // Act
        //     var addedEntitiesCount = dbSeeder.SeedBookEntitiesFromJson(pathToSeedData);

        //     // Assert
        //     Assert.NotEqual(0, addedEntitiesCount);
        // }

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
                BookIsbn13 = "1234567890123"
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

        // private static Mock<DbSet<T>> GetQueryableDbSet<T>(List<T> sourceList) where T : class
        // {
        //     var queryable = sourceList.AsQueryable();

        //     var dbSet = new Mock<DbSet<T>>();
        //     dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        //     dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        //     dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        //     dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        //     dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

        //     return dbSet;
        // }
    }
}
