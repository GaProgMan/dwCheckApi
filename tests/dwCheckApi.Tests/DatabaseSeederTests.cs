using Xunit;
using System.IO;
using System;
using System.Threading.Tasks;
using dwCheckApi.Persistence;
using dwCheckApi.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace dwCheckApi.Tests
{
    public class DatabaseSeederTests
    {
        private readonly DbContextOptions<DwContext> _contextOptions;
        public DatabaseSeederTests()
        {
            _contextOptions = new DbContextOptionsBuilder<DwContext>()
                .UseInMemoryDatabase("dwCheckApi.Tests.InMemoryContext")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }
        [Fact]
        public async void DbSeeder_SeedBookData_NoDataSupplied_ShouldThrowException()
        {
            // Arrange
            await using var context = new DwContext(_contextOptions);

            // Act & Assert
            var dbSeeder = new DatabaseSeeder(context);
            var argEx = await Assert.ThrowsAsync<ArgumentException>(() =>
                dbSeeder.SeedBookEntitiesFromJson(string.Empty));
        }
        
        [Fact]
        public async Task DbSeeder_SeedBookData_DataSupplied_ShouldNotThrowException()
        {
            // Arrange
            await using var context = new DwContext(_contextOptions);
            
            var testJsonDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedData");
            var pathToSeedData = Path.Combine(testJsonDirectory, "TestBookSeedData.json");
            var dbSeeder = new DatabaseSeeder(context);
            
            // Act
            var entitiesAdded = await dbSeeder.SeedBookEntitiesFromJson(pathToSeedData);
            
            // Assert
            Assert.NotEqual(0, entitiesAdded);
        }
    }
}