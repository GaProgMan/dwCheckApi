using System;
using System.Collections.Generic;
using System.Linq;
using dwCheckApi.DTO.Helpers;
using Xunit;

namespace dwCheckApi.Tests.ViewModelMappers
{
    public class CharacterViewModelMapperTests
    {
        [Fact]
        public void Given_CharacterDbModel_Returns_ViewModel()
        {
            // Arrange
            var characterName = Guid.NewGuid().ToString();
            var books = new Dictionary<int, string>
            {
                // intentionally added out of order ot test the ordering of the final Dictionary
                { 2, Guid.NewGuid().ToString() },
                { 1, Guid.NewGuid().ToString() }
            };
            
            // Act
            var response = CharacterViewModelHelpers.ConvertToViewModel(characterName, books);

            // Assert
            Assert.Equal(response.CharacterName, characterName);
            Assert.Equal(response.Books.Count, books.Count);

            var first = response.Books.First();
            Assert.Equal(1, first.Key);
        }
    }
}