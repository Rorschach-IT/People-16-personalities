using Microsoft.AspNetCore.Mvc;
using PeoplePersonalities.Models;
using PeoplePersonalities.Services;
using System.Diagnostics;

namespace PeoplePersonalities.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonPersonalityService _service;
        private readonly IAggregateDataService _aggregateDataService;

        public HomeController(
            IPersonPersonalityService service,
            IAggregateDataService aggregateDataService)
        {
            _service = service;
            _aggregateDataService = aggregateDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string? type)
        {
            var model = new HomeIndexViewModel
            {
                Items = await _service.GetAllAsync(type),
                Types = await _aggregateDataService.GetDistinctTypesAsync(),
                SelectedType = type
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}