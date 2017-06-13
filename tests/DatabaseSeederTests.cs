using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using dwCheckApi.DatabaseContexts;
using dwCheckApi.DatabaseTools;
using dwCheckApi.Helpers;
using dwCheckApi.Models;
using dwCheckApi.ViewModels;
using dwCheckApt.Tests.Helpers;
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