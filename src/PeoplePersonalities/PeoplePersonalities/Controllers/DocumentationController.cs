using Microsoft.AspNetCore.Mvc;

namespace PeoplePersonalities.Controllers
{
    public class DocumentationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Documentation/Index.cshtml");
        }
    }
}
