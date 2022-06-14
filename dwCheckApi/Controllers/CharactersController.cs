using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dwCheckApi.DAL;
using dwCheckApi.DTO.Helpers;
using dwCheckApi.DTO.ViewModels;
using Microsoft.AspNetCore.Http;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    [Produces("application/json")]
    public class CharactersController : BaseController
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        /// <summary>
        /// Used to get a Character record by its ID
        /// </summary>
        /// <param name="id">The ID fo the Character record to return</param>
        /// <returns>
        /// If a Character record can be found, then a <see cref="BaseController.SingleResult{T}"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.CharacterViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(typeof(SingleResult<CharacterViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var dbCharacter = _characterService.GetById(id);
            if (dbCharacter == null)
            {
                return NotFoundResponse("Character not found");
            }

            return Ok(new SingleResult<CharacterViewModel>
            {
                Success = true,
                Result = CharacterViewModelHelpers.ConvertToViewModel(dbCharacter.CharacterName)
            });
        }

        /// <summary>
        /// Used to get a Character record by its name
        /// </summary>
        /// <param name="characterName">The name of the Character record to return</param>
        /// <returns>
        /// If a Character record can be found, then a <see cref="BaseController.SingleResult{T}"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.CharacterViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        [HttpGet("GetByName")]
        [ProducesResponseType(typeof(SingleResult<CharacterViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetByName(string characterName)
        {
            if (string.IsNullOrWhiteSpace(characterName))
            {
                return NotFoundResponse("Character name is required");
            }

            var character = _characterService.GetByName(characterName);

            if (character == null)
            {
                return NotFoundResponse("Character not found");
            }

            return Ok(new SingleResult<CharacterViewModel>
            {
                Success = true,
                Result = CharacterViewModelHelpers.ConvertToViewModel(character.CharacterName)
            });
        }

        /// <summary>
        /// Used to search Character records by their name
        /// </summary>
        /// <param name="searchString">The string to use when searching for Character records</param>
        /// <returns>
        /// If a Character records can be found, then a <see cref="BaseController.SingleResult{T}"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.CharacterViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        [HttpGet("Search")]
        [ProducesResponseType(typeof(MultipleResult<CharacterViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult Search(string searchString)
        {
            var foundCharacters = _characterService
                .Search(searchString).ToList();
            if (!foundCharacters.Any())
            {
                return NotFoundResponse("No Characters found");
            }

            var flattenedCharacters = foundCharacters
                .Select(character => CharacterViewModelHelpers
                    .ConvertToViewModel(character.Key,
                        character.ToDictionary(bc => bc.Book.BookOrdinal, bc => bc.Book.BookName)));

            return Ok(new MultipleResult<CharacterViewModel>
            {
                Success = true,
                Result = flattenedCharacters.ToList()
            });
        }
    }
}