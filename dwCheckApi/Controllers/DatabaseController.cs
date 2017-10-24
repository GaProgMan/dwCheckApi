using System.Linq;
using System.Net;
using System.Threading.Tasks;
using dwCheckApi.DAL;
using Microsoft.AspNetCore.Mvc;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    public class DatabaseController : BaseController
    {
        private readonly IDatabaseService _databaseService;
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

        [HttpGet("ApplyBookCoverArt")]
        public async Task<JsonResult> ApplyBookCoverArt()
        {
            var relevantBooks = _databaseService.BooksWithoutCoverBytes();

            using (var webclient = new WebClient())
            {
                foreach (var book in relevantBooks.ToList())
                {
                    var coverData = webclient.DownloadData(book.BookCoverImageUrl);
                    book.BookCoverImage = coverData;
                }
            }

            var updatedRecordCount = await _databaseService.SaveAnyChanges();

            return MessageResult($"{updatedRecordCount} entities updated");
        }
    }
}