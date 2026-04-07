using Microsoft.AspNetCore.Mvc;

namespace PeoplePersonalities.Controllers
{
    [Route("Documentation")]
    public class DocumentationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Documentation/Index.cshtml");
        }
    }
}
