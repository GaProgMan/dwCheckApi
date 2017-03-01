using dwCheckApi.Services;
using dwCheckApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    public class DatabaseController : BaseController
    {
        private IDatabaseService _databaseService;

        public DatabaseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("SeedData")]
        public JsonResult SeedData()
        {
            var entitiesAdded = _databaseService.SeedDatabase();

            return MessageResult($"Number of new entities added: {entitiesAdded}");

        }

        [HttpGet("DropData")]
        public JsonResult DropData()
        {
            var success = _databaseService.ClearDatabase();

            return MessageResult("Database tabled dropped and recreated", success);
        }
    }
}