using Microsoft.AspNetCore.Mvc;
using dwCheckApi.Helpers;
using Microsoft.AspNetCore.Http;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    [Produces("application/json")]
    public class VersionController : BaseController
    {
        /// <summary>
        /// Gets the semver formatted version number for the application
        /// </summary>
        /// <returns>
        /// A string representing the semver formatted version number for the application
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(new SingleResult<string>
            {
                Success = true,
                Result = CommonHelpers.GetVersionNumber()
            });
        }
    }
}