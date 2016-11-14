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

        [HttpGet("All")]
        public JsonResult GetAll()
        {
            // This method will be computationally expensive and
            // the payload will be massive.
            // Included here for testing purposes
            var books = _characterService.GetAll();
            return Json(books.ToList());
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