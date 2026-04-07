using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PeoplePersonalities.Models;
using PeoplePersonalities.Services;
using System.Globalization;

namespace PeoplePersonalities.Controllers
{
    public class AddPersonController : Controller
    {
        private readonly IAddPersonService _addPersonService;

        public AddPersonController(IAddPersonService addPersonService)
        {
            _addPersonService = addPersonService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/AddPerson/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerson(PersonPersonality model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            model.FirstName = CapitalizeAndTrim(model.FirstName);
            model.LastName = CapitalizeAndTrim(model.LastName);
            model.Type = model.Type.Trim().ToUpperInvariant();

            var exists = await _addPersonService.ExistsAsync(model.FirstName, model.LastName);

            if (exists)
            {
                ModelState.AddModelError(string.Empty, "Osoba o takim imieniu i nazwisku już istnieje w bazie.");
                return View("Index", model);
            }

            try
            {
                await _addPersonService.AddAsync(model);
            }
            catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
            {
                ModelState.AddModelError(string.Empty, "Osoba o takim imieniu i nazwisku już istnieje w bazie.");
                return View("Index", model);
            }

            return RedirectToAction("Index", "Home");
        }

        private static string CapitalizeAndTrim(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim();

            if (value.Length == 1)
                return value.ToUpper(new CultureInfo("pl-PL"));

            var culture = new CultureInfo("pl-PL");
            return char.ToUpper(value[0], culture) + value.Substring(1).ToLower(culture);
        }
    }
}