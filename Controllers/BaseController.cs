using dwCheckApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace dwCheckApi.Controllers
{
    public class BaseController : Controller
    {
        public string IncorrectUseOfApi()
        {
            return CommonHelpers.IncorrectUsageOfApi();
        }
    }
}