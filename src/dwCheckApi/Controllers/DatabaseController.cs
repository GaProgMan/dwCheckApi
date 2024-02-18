using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using dwCheckApi.DAL;
using dwCheckApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    [Produces("application/json")]
    public class DatabaseController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseService _databaseService;
        
        public DatabaseController(IConfiguration configuration, IDatabaseService databaseService)
        {
            _configuration = configuration;
            _databaseService = databaseService;
        }

        /// <summary>
        /// Used to Seed the Database (using JSON files which are included with the application)
        /// </summary>
        /// <returns>
        /// A <see cref="BaseController.SingleResult{T}"/> with either the number of entities which
        /// were added to the database, or an exception message.
        /// </returns>
        [HttpGet("SeedData")]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SeedData()
        {
            try
            {
                var entitiesAdded = _databaseService.SeedDatabase();
                return Ok(new SingleResult<string>
                {
                    Success = true,
                    Result = $"Number of new entities added: {entitiesAdded}"
                });
            }
            catch (Exception ex)
            {
                return ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Used to drop all current data from the database and recreate any tables
        /// </summary>
        /// <param name="secret">
        /// A passphrase like secret to ensure that a Drop Data action should take place
        /// </param>
        /// <returns>
        /// A <see cref="BaseController.SingleResult{T}"/> indicating whether we could clear
        /// the database or not at this time
        /// </returns>
        [HttpDelete("DropData")]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DropData(string secret = null)
        {
            if (!SecretChecker.CheckUserSuppliedSecretValue(secret,
                _configuration["dropDatabaseSecretValue"]))
            {
                return ErrorResponse(StatusCodes.Status401Unauthorized,"Incorrect secret");
            }
        
            return _databaseService.ClearDatabase()
                ? Ok(new SingleResult<string>
                {
                    Success = true,
                    Result = "Database tabled dropped and recreated"
                })
                : ErrorResponse(StatusCodes.Status500InternalServerError, "Unable to clear database at this time");
        }

        /// <summary>
        /// Used to prepare and apply all Book cover art (as Base64 strings)
        /// </summary>
        /// <returns>
        /// A <see cref="BaseController.SingleResult{T}"/> with the number of entities which were altered.
        /// </returns>
        [HttpGet("ApplyBookCoverArt")]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ApplyBookCoverArt()
        {
            var relevantBooks = _databaseService.BooksWithoutCoverBytes().ToList();

            if (!relevantBooks.Any())
            {
                return Ok(new SingleResult<string>()
                {
                    Success = true,
                    Result = "No records to update"
                });
            }

            try
            {
                using var client = new HttpClient();
                foreach (var book in relevantBooks)
                {
                    var coverData = await client.GetByteArrayAsync(book.BookCoverImageUrl);
                    book.BookCoverImage = coverData;
                }
            }
            catch (Exception ex)
            {
                return ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }

            var updatedRecordCount = await _databaseService.SaveAnyChanges();

            return Ok(new SingleResult<string>
            {
                Success = true,
                Result =$"{updatedRecordCount} entities updated" 
            });
        }
    }
}