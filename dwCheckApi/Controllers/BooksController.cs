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
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult{T}"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /1
        ///
        /// </remarks>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(typeof(SingleResult<BookViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetByOrdinal(int id)
        {
            var book = _bookService.FindByOrdinal(id);
            if (book == null)
            {
                return NotFoundResponse("Not found");
            }

            return Ok(new SingleResult<BookViewModel>
            {
                Success = true,
                Result = BookViewModelHelpers.ConvertToViewModel(book)
            });
        }

        /// <summary>
        /// Used to get a Book by its title
        /// </summary>
        /// <param name="bookName">The name to use when searching for a book</param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult{T}"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetByName?bookName=night%20watch
        ///
        /// </remarks>
        /// <response code="200">The book object which matches on the supplied title</response>
        /// <response code="404">The requested book could not be found</response>
        [HttpGet("GetByName")]
        [ProducesResponseType(typeof(SingleResult<BookViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetByName(string bookName)
        {
            if (string.IsNullOrWhiteSpace(bookName))
            {
                return NotFoundResponse("Book name is required");
            }

            var book = _bookService.GetByName(bookName);

            if (book == null)
            {
                return NotFoundResponse("No book with that name could be found");
            }

            return Ok(new SingleResult<BookViewModel>
            {
                Success = true,
                Result = BookViewModelHelpers.ConvertToViewModel(book)
            });
        }

        /// <summary>
        /// Used to search all Book records with a given search string (searches against Book
        /// name, description and ISBN numbers)
        /// </summary>
        /// <param name="searchString">The search string to use</param>
        /// <returns>
        /// If Book records can be found, then a <see cref="BaseController.MultipleResult{T}"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no records can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Search?searchString=night
        ///
        /// </remarks>
        [HttpGet("Search")]
        [ProducesResponseType(typeof(MultipleResult<BookViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult Search(string searchString)
        {
            var dbBooks = _bookService.Search(searchString).ToList();

            if (!dbBooks.Any())
            {
                return NotFoundResponse();
            }

            return Ok(new MultipleResult<BookViewModel>
            {
                Success = true,
                Result = BookViewModelHelpers.ConvertToViewModels(dbBooks)
            });
        }

        /// <summary>
        /// Used to get all Book records within a Series, by the series ID
        /// </summary>
        /// <param name="seriesId">The ID of the series</param>
        /// <returns>
        /// If Book records can be found, then a <see cref="BaseController.MultipleResult{T}"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no records can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Series/6
        ///
        /// </remarks>
        /// <response code="200">All books in the requested series</response>
        /// <response code="404">The series with the requested number could not be found</response>
        [HttpGet("Series/{seriesId}")]
        [ProducesResponseType(typeof(MultipleResult<BookViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetForSeries(int seriesId)
        {
            var dbBooks = _bookService.Series(seriesId).ToList();

            if (!dbBooks.Any())
            {
                return NotFoundResponse();
            }

            return Ok(new
            {
                Success = true,
                Result = BookViewModelHelpers.ConvertToViewModels(dbBooks)
            });
        }

        /// <summary>
        /// Used to get the Cover Art for a Book record with a given ID
        /// </summary>
        /// <param name="bookId">
        /// The Bookd ID for the relevant book record (this is the identity, not the ordinal)
        /// </param>
        /// <returns>
        /// If a Book record can be found, then a <see cref="BaseController.SingleResult{T}"/>
        /// is returned, which contains a <see cref="dwCheckApi.DTO.ViewModels.BookCoverViewModel"/>.
        /// If no record can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetBookCover/29
        ///
        /// </remarks>
        /// <response code="200">An object representing the requested book's cover art as a Base64 string</response>
        /// <response code="404">The requested book could not be found</response>
        [HttpGet("GetBookCover/{bookId}")]
        [ProducesResponseType(typeof(SingleResult<BookCoverViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetBookCover(int bookId)
        {
            var dbBook = _bookService.FindById(bookId);
            if (dbBook == null)
            {
                return NotFoundResponse();
            }

            return Ok(new
            {
                Success = true,
                Result = BookViewModelHelpers.ConvertToBookCoverViewModel(dbBook)
            });
        }
        
        /// <summary>
        /// Returns an array of all the books in the database 
        /// </summary>
        /// <returns>
        /// If Book records can be found, then a <see cref="BaseController.MultipleResult{T}"/>
        /// is returned, which contains a collection of <see cref="dwCheckApi.DTO.ViewModels.BookViewModel"/>.
        /// If no records can be found, then an <see cref="BaseController.NotFoundResponse"/> is returned
        /// </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /All
        ///
        /// </remarks>
        /// <response code="200">All books in the database</response>
        /// <response code="404">No books could be found in the database</response>
        [HttpGet("All")]
        [ProducesResponseType(typeof(MultipleResult<BookViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SingleResult<string>), StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var books = _bookService.GetAll().ToList();
            if (!books.Any())
            {
                return NotFoundResponse("No books found");
            }

            return Ok(BookViewModelHelpers.ConvertToViewModels(books));
        }
    }
}