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
            if (dbCharacter != null)
            {
                var viewModel = CharacterViewModelHelpers.ConvertToviewModel(dbCharacter);
                return Json(viewModel);
            }
            return Json("Not found");
        }

        [HttpGet("Search")]
        public JsonResult Search(string searchString)
        {
            var characters = _characterService
                .Search(searchString);

            if (characters.Any())
            {
                var viewModels = CharacterViewModelHelpers
                    .ConvertToViewModels(characters.ToList());
                    
                return Json(viewModels);
            }

            return Json("Not Found");
        }
    }
}