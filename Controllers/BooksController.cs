using dwCheckApi.Helpers;
using dwCheckApi.ViewModels;
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
            if (book != null)
            {
                var viewModel = BookViewModelHelpers.ConvertToViewModel(book);
                return Json(viewModel);
            }
            return Json("Not found");
        }

        // Search?searchKey
        [HttpGet("Search")]
        public JsonResult Search(string searchString)
        {
            var dbBooks = _bookService.Search(searchString).ToList();
            if(dbBooks.Any())
            {
                var viewModelBooks = BookViewModelHelpers.ConvertToViewModels(dbBooks);
                return Json(viewModelBooks);
            }

            return Json("Not found");
            
        }
    }
}
