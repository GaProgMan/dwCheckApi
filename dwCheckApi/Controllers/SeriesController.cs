using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dwCheckApi.DAL;
using dwCheckApi.DTO.Helpers;

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
        /// If a Series record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.SeriesViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetById(int id)
        {
            var dbSeries = _seriesService.GetById(id);
            if (dbSeries == null)
            {
                return ErrorResponse("Not found");
            }
            
            return SingleResult(SeriesViewModelHelpers.ConvertToViewModel(dbSeries));
        }

        /// <summary>
        /// Used to get a Series record by its name
        /// </summary>
        /// <param name="seriesName">The name of the Series record to return</param>
        /// <returns>
        /// If a Series record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.SeriesViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("GetByName")]
        public JsonResult GetByName(string seriesName)
        {
            if (string.IsNullOrWhiteSpace(seriesName))
            {
                return ErrorResponse("Series name is required");
            }

            var series = _seriesService.GetByName(seriesName);

            if (series == null)
            {
                return ErrorResponse("No Series found");
            }

            return SingleResult(SeriesViewModelHelpers.ConvertToViewModel(series));
        }

        /// <summary>
        /// Used to search Series records by their name
        /// </summary>
        /// <param name="searchString">The string to use when searching for Series</param>
        /// <returns>
        /// If a Series records can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.SeriesViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Search")]
        public JsonResult Search(string searchString)
        {
            var series = _seriesService
                .Search(searchString);

            if (!series.Any())
            {
                return ErrorResponse("Not Found");
            }
                            
            return MultipleResults(SeriesViewModelHelpers.ConvertToViewModels(series.ToList()));
        }
    }
}
