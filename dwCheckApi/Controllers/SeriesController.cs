using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dwCheckApi.DAL;
using dwCheckApi.DTO.Helpers;
using dwCheckApi.DTO.ViewModels;
using Microsoft.AspNetCore.Http;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    [Produces("application/json")]
    public class SeriesController : BaseController
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        /// <summary>
        /// Used to get a Series record by its ID
        /// </summary>
        /// <param name="id">The ID of the Series Record</param>
        /// <returns>
        /// If a Series record can be found, then a <see cref="BaseController.SingleResult{T}"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.SeriesViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(typeof(SingleResult<SeriesViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var dbSeries = _seriesService.GetById(id);
            if (dbSeries == null)
            {
                return NotFoundResponse("Not found");
            }

            return Ok(new SingleResult<SeriesViewModel>
            {
                Success = true,
                Result = SeriesViewModelHelpers.ConvertToViewModel(dbSeries)
            });
        }

        /// <summary>
        /// Used to get a Series record by its name
        /// </summary>
        /// <param name="seriesName">The name of the Series record to return</param>
        /// <returns>
        /// If a Series record can be found, then a <see cref="BaseController.SingleResult{T}"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.SeriesViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        [HttpGet("GetByName")]
        [ProducesResponseType(typeof(SingleResult<SeriesViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetByName(string seriesName)
        {
            if (string.IsNullOrWhiteSpace(seriesName))
            {
                return NotFoundResponse("Series name is required");
            }

            var series = _seriesService.GetByName(seriesName);

            if (series == null)
            {
                return NotFoundResponse("No Series found");
            }

            return Ok(new SingleResult<SeriesViewModel>
            {
                Success = true,
                Result = SeriesViewModelHelpers.ConvertToViewModel(series)
            });
        }

        /// <summary>
        /// Used to search Series records by their name
        /// </summary>
        /// <param name="searchString">The string to use when searching for Series</param>
        /// <returns>
        /// If a Series records can be found, then a <see cref="BaseController.MultipleResult{T}"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.SeriesViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        [HttpGet("Search")]
        [ProducesResponseType(typeof(MultipleResult<SeriesViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult Search(string searchString)
        {
            var series = _seriesService
                .Search(searchString).ToList();

            if (!series.Any())
            {
                return NotFoundResponse($"No series found for supplied search string: {searchString}");
            }

            return Ok(new MultipleResult<SeriesViewModel>
            {
                Success = true,
                Result = SeriesViewModelHelpers.ConvertToViewModels(series.ToList())
            });
        }
    }
}
