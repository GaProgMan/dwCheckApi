using Microsoft.AspNetCore.Mvc;

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