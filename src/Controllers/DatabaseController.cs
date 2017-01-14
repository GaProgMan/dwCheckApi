using dwCheckApi.Services;
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
        public void SeedData()
        {
            _databaseService.SeedDatabase();
        }

        [HttpGet("DropData")]
        public void DropData()
        {
            _databaseService.ClearDatabase();
        }
    }
}