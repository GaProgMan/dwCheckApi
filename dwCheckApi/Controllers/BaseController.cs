using System.Collections.Generic;
using dwCheckApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace dwCheckApi.Controllers
{
    public class BaseController : Controller
    {
        protected record SingleResult<T> where T : class
        {
            public bool Success { get; set; }
            public T Result { get; set; }
        }

        protected record MultipleResult<T> where T : class
        {
            public bool Success { get; set; }
            public List<T> Result { get; set; }
        }

        protected static string IncorrectUseOfApi()
        {
            return CommonHelpers.IncorrectUsageOfApi();
        }
        
        protected IActionResult NotFoundResponse(string message = "Not Found")
        {
            return NotFound(new SingleResult<string>{
                Success = false,
                Result = message
            });
        }

        protected static IActionResult ErrorResponse(int statusCode, string message = "Internal server error")
        {
            return new StatusCodeResult(statusCode);
        }
    }
}