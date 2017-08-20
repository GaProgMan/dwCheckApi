using System.Collections.Generic;
using dwCheckApi.DatabaseContexts;
using dwCheckApi.DatabaseTools;
using dwCheckApi.Models;
using dwCheckApt.Tests.Helpers;
using Moq;
using Xunit;
using System.IO;
using System;

namespace dwCheckApi.Tests
{
    public class DatabaseSeederTests
    {
        [Fact]
        public void DbSeeder_SeedBookData_NoDataSupplied_ShouldThrowException()
        {
            // Arrange
            var bookList = new List<Book>();
            var mockBookSet = DbSetHelpers.GetQueryableDbSet(bookList);
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
            var mockBookSet = DbSetHelpers.GetQueryableDbSet(bookList);
            var mockset = new Mock<IDwContext>();
            mockset.Setup(m => m.Books).Returns(mockBookSet.Object);
            var testJsonDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedData");
            var pathToSeedData = Path.Combine(testJsonDirectory, "TestBookSeedData.json");
            
            // Act & Assert
            var dbSeeder = new DatabaseSeeder(mockset.Object);
            
            dbSeeder.SeedBookEntitiesFromJson(pathToSeedData);
        }
    }
}