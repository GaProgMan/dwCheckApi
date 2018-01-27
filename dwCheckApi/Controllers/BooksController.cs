using Microsoft.AspNetCore.Mvc;
using System.Linq;
using dwCheckApi.DAL;
using dwCheckApi.DTO.Helpers;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    [Produces("application/json")]
    public class BooksController : BaseController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }


        /// <summary>
        /// Used to get a Book record by its ordinal (the order in which it was released)
        /// </summary>
        /// <param name="id">The ordinal of a Book to return</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Get/{id}")]
        public JsonResult GetByOrdinal(int id)
        {
            var book = _bookService.FindByOrdinal(id);
            if (book == null)
            {
                return ErrorResponse("Not found");
            }

            return SingleResult(BookViewModelHelpers.ConvertToViewModel(book));
        }

        /// <summary>
        /// Used to get a Book by its title
        /// </summary>
        /// <param name="bookName">The name to use when searching for a book</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("GetByName")]
        public JsonResult GetByName(string bookName)
        {
            if (string.IsNullOrWhiteSpace(bookName))
            {
                return ErrorResponse("Book name is required");
            }
            var book = _bookService.GetByName(bookName);

            if (book == null)
            {
                return ErrorResponse("No book with that name could be found");
            }

            return SingleResult(BookViewModelHelpers.ConvertToViewModel(book));
        }

        /// <summary>
        /// Used to search all Book records with a given search string (searches against Book
        /// name, description and ISBN numbers)
        /// </summary>
        /// <param name="searchString">The search string to use</param>
        /// <returns>
        /// If Book records can be found, then a <see cref="BaseController.MultipleResults"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no records can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Search")]
        public JsonResult Search(string searchString)
        {
            var dbBooks = _bookService.Search(searchString).ToList();

            if (!dbBooks.Any())
            {
                return ErrorResponse();
            }

            return MultipleResults(BookViewModelHelpers.ConvertToViewModels(dbBooks));
        }

        /// <summary>
        /// Used to get all Book records within a Series, by the series ID
        /// </summary>
        /// <param name="seriesId">The ID of the series</param>
        /// <returns>
        /// If Book records can be found, then a <see cref="BaseController.MultipleResults"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no records can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("Series/{seriesId}")]
        public JsonResult GetForSeries(int seriesId)
        {
            var dbBooks = _bookService.Series(seriesId).ToList();

            if (!dbBooks.Any())
            {
                return ErrorResponse();
            }
            
            return MultipleResults(BookViewModelHelpers.ConvertToBaseViewModels(dbBooks));
        }
        
        /// <summary>
        /// Used to get the Cover Art for a Book record with a given ID
        /// </summary>
        /// <param name="bookId">
        /// The Bookd ID for the relevant book record (this is the identity, not the ordinal)
        /// </param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.BookCoverViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.ErrorResponse"/> is returned
        /// </returns>
        [HttpGet("GetBookCover/{bookId}")]
        public JsonResult GetBookCover(int bookId)
        {
            var dbBook = _bookService.FindById(bookId);
            if (dbBook == null)
            {
                return ErrorResponse();
            }

            return SingleResult(BookViewModelHelpers.ConverToBookCoverViewModel(dbBook));
        }
    }
}