using System.Collections.Generic;
using dwCheckApi.DTO.ViewModels;
using dwCheckApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace dwCheckApi.Controllers
{
    public class BaseController : Controller
    {
        protected string IncorrectUseOfApi()
        {
            return CommonHelpers.IncorrectUsageOfApi();
        }
        
        protected JsonResult ErrorResponse(string message = "Not Found")
        {
            return Json (new {
                Success = false,
                Result = message
            });
        }

        protected JsonResult MessageResult(string message, bool success = true)
        {
            return Json(new {
                Success = success,
                Result = message
            });
        }

        protected JsonResult SingleResult(BaseViewModel singleResult)
        {
            return Json(new {
                Success = true,
                Result = singleResult
            });
        }

        protected JsonResult MultipleResults(IEnumerable<BaseViewModel> multipleResults)
        {
            return Json (new {
                Success = true,
                Result = multipleResults
            });
        }
    }
}