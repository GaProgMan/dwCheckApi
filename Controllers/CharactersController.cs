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
            var character = _characterService.GetById(id);
            return Json(character);
        }

        [HttpGet("Search")]
        public JsonResult Search(string searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var characters = _characterService
                    .Search(searchString)
                    .ToList();
                return Json(characters);
            }
            return Json("No results found");
        }
    }
}