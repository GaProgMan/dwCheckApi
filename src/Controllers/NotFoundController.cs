using dwCheckApi.ViewModels;
using dwCheckApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace dwCheckApi.Controllers
{
    [Route("/")]
    public class NotFoundController : BaseController
    {
        [HttpGet]
        public string Get()
        {
            return IncorrectUseOfApi();
        }
    }


}