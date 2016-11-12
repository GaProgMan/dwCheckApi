using dwCheckApi.Models;
using dwCheckApi.Services;

using Microsoft.AspNetCore.Mvc;

namespace dwCheckApi.Controllers
{
    [Route("/")]
    public class BooksController : Controller
    {
        private IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public string Get()
        {
            return "Incorrect use of API.";
        }

        // GET /5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var book = _bookService.FindByOrdinal(id);
            if (book != null)
            {
                return $"{book.BookName}";
            }
            return "Not found";
        }
    }
}
