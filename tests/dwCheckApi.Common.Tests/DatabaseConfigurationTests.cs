using NSubstitute;

namespace dwCheckApi.Common.Tests
{

    public class DatabaseConfigurationTests
    {
        private readonly DatabaseConfiguration _databaseConfigurationMock = Substitute.For<DatabaseConfiguration>();

        [Fact]
        public void GetDatabaseConnectionString_ReturnsNonEmptyConnectionString()
        {
            var connectionString = _databaseConfigurationMock.GetDatabaseConnectionString();
            Assert.IsAssignableFrom<string>(connectionString);
            Assert.NotEmpty(connectionString);
        }
    }
}