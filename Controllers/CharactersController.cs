using dwCheckApi.Helpers;
using dwCheckApi.ViewModels;
using dwCheckApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    public class CharactersController : BaseController
    {
        private ICharacterService _characterService;

        public CharactersController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public string Get()
        {
            return IncorrectUseOfApi();
        }

        // GET/5
        [HttpGet("Get/{id}")]
        public JsonResult GetById(int id)
        {
            var dbCharacter = _characterService.GetById(id);
            if (dbCharacter == null)
            {
                return Json("Not found");
            }
            var viewModel = CharacterViewModelHelpers.ConvertToviewModel(dbCharacter);
            return Json(viewModel);
        }

        [HttpGet("GetByName")]
        public JsonResult GetByName(string characterName)
        {
            if (string.IsNullOrWhiteSpace(characterName))
            {
                return Json("Character name is required");
            }

            var character = _characterService.GetByName(characterName);

            if (character == null)
            {
                return Json ("No character found");
            }

            return Json(CharacterViewModelHelpers.ConvertToviewModel(character));
        }

        [HttpGet("Search")]
        public JsonResult Search(string searchString)
        {
            var characters = _characterService
                .Search(searchString);

            if (!characters.Any())
            {
                return Json("Not Found");
            }

            var viewModels = CharacterViewModelHelpers.ConvertToViewModels(characters.ToList());                    
            return Json(viewModels);
        }
    }
}