using System;
using dwCheckApi.Helpers;
using Xunit;

namespace dwCheckApi.Tests.Helpers
{
    public class SecretCheckerTests
    {
        [Theory]
        [InlineData(null, "something")]
        [InlineData("", "something")]
        [InlineData("something", null)]
        [InlineData("something", "")]
        public void CheckUserSuppliedSecretValue_A_Null_Returns_False(string userSuppliedValue, string secretValue)
        {
            // Arrange

            // Act
            var response = SecretChecker.CheckUserSuppliedSecretValue(userSuppliedValue, secretValue);
            
            // Assert
            Assert.False(response);
        }

        [Fact]
        public void CheckUserSuppliedSecretValue_Matching_Strings_Returns_True()
        {
            // Arrange
            string secretValue;

            var userSuppliedValue = secretValue = Guid.NewGuid().ToString();

            // Act
            var response = SecretChecker.CheckUserSuppliedSecretValue(userSuppliedValue, secretValue);
            
            // Assert
            Assert.True(response);
        }
    }
}