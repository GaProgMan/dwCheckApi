using dwCheckApi.Helpers;
using dwCheckApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace dwCheckApi.Controllers
{
    [Route("/[controller]")]
    public class BooksController : BaseController
    {
        private IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public string Get()
        {
            return IncorrectUseOfApi();
        }

        // Get/5
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

        // Search?searchKey
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
    }
}