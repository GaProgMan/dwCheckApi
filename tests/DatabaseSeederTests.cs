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
    public class DatabaseSeederTests
    {
        
        [Fact]
        public void DbSeeder_SeedBookData_NoDataSupplied_ShouldThrowException()
        {
            // Arrange
            var bookList = new List<Book>();
            var mockBookSet = GetQueryableDbSet(bookList);
            var mockset = new Mock<IDwContext>();
            mockset.Setup(m => m.Books).Returns(mockBookSet.Object);

            // Act & Assert
            var dbSeeder = new DatabaseSeeder(mockset.Object);
            ArgumentException argEx = Assert.Throws<ArgumentException>(() =>
                dbSeeder.SeedBookEntitiesFromJson(string.Empty));
        }

        [Fact]
        public void DbSeeder_SeedBookData_DataSupplied_ShouldNotThrowException()
        {
            // Arrange
            // TODO Add an interface here, to mock stuff properly
            var bookList = new List<Book>();
            var mockBookSet = GetQueryableDbSet(bookList);
            var mockset = new Mock<IDwContext>();
            mockset.Setup(m => m.Books).Returns(mockBookSet.Object);
            var testJsonDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedData");
            var pathToSeedData = Path.Combine(testJsonDirectory, "TestBookSeedData.json");
            
            
            // Act & Assert
            var dbSeeder = new DatabaseSeeder(mockset.Object);
            
            dbSeeder.SeedBookEntitiesFromJson(pathToSeedData);
        }

        private static Mock<DbSet<T>> GetQueryableDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet;
        }
    }
}