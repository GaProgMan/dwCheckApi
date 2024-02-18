using dwCheckApi.Helpers;
using Xunit;

namespace dwCheckApi.Tests.Helpers
{
    public class CommonHelperTests
    {
        [Fact]
        public void IncorrectUsageOfApi_Returns_NonNull_String()
        {
            // Arrange
            
            // Act
            var response = CommonHelpers.IncorrectUsageOfApi();
            
            // Assert
            Assert.NotEmpty(response);
            Assert.Contains("Incorrect usage of API", response);
        }

        [Fact]
        public void GetVersionNumber_Returns_NonNull_String()
        {
            // Arrange
            
            // Act
            var response = CommonHelpers.GetVersionNumber();
            
            // Assert
            Assert.NotEmpty(response);
        }
    }
}