using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dwCheckApi.DAL;
using dwCheckApi.DTO.Helpers;

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
        /// If a Character record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.CharacterViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetById(int id)
        {
            var dbCharacter = _characterService.GetById(id);
            return dbCharacter == null
                ? ErrorResponse("Not found")
                : SingleResult(CharacterViewModelHelpers.ConvertToViewModel(dbCharacter.CharacterName));
        }

        /// <summary>
        /// Used to get a Character record by its name
        /// </summary>
        /// <param name="characterName">The name of the Character record to return</param>
        /// <returns>
        /// If a Character record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.CharacterViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("GetByName")]
        public JsonResult GetByName(string characterName)
        {
            if (string.IsNullOrWhiteSpace(characterName))
            {
                return ErrorResponse("Character name is required");
            }

            var character = _characterService.GetByName(characterName);

            return character == null
                ? ErrorResponse("No character found")
                : SingleResult(CharacterViewModelHelpers.ConvertToViewModel(character.CharacterName));
        }

        /// <summary>
        /// Used to search Character records by their name
        /// </summary>
        /// <param name="searchString">The string to use when searching for Character records</param>
        /// <returns>
        /// If a Character records can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.CharacterViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Search")]
        public JsonResult Search(string searchString)
        {
            var foundCharacters = _characterService
                .Search(searchString).ToList();

           
            return !foundCharacters.Any()
                ? ErrorResponse("No Characters found")
                : MultipleResults(foundCharacters
                    .Select(character => CharacterViewModelHelpers
                        .ConvertToViewModel(character.Key,
                            character.ToDictionary(bc => bc.Book.BookOrdinal, bc => bc.Book.BookName))));
        }
    }
}