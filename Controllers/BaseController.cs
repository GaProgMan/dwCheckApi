using dwCheckApi.ViewModels;
using dwCheckApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace dwCheckApi.Controllers
{
    public class BaseController : Controller
    {
        /// Hard code this list for now, as .NET Core 1.0
        /// doesn't have reflection
        public string IncorrectUseOfApi()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append($"Incorrect useage of API{Environment.NewLine}");
            sb.Append($"The following functions are available for Books:{Environment.NewLine}");
            sb.Append($"\t'/Books/FindByOrdinal' - Returns a single book, by it's ordinal (release order){Environment.NewLine}");
            sb.Append($"\t'/Books/Search' - Searches all Books for a search string (searchKey)");
            sb.Append($"The following functions are available for Characters:{Environment.NewLine}");
            sb.Append($"\t'/Characters/GetAll' - Returns all Characters in Database{Environment.NewLine}");
            sb.Append($"\t'/Characters/Search' - Searches all Characters for a search string (searchKey)");
            return sb.ToString();
        }
    }
}