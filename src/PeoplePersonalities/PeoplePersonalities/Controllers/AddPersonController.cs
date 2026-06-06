using Microsoft.AspNetCore.Mvc;
using PeoplePersonalities.Models;
using PeoplePersonalities.Services.Interfaces;

namespace PeoplePersonalities.Controllers;

public class AddPersonController(IAddPersonService addPersonService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Views/AddPerson/Index.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(PersonPersonality model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        var result = await addPersonService.AddPersonAsync(model);

        if (result.Success) return RedirectToAction("Index", "Home");
        ModelState.AddModelError(string.Empty, result.ErrorMessage!);
        return View("Index", model);
    }
}
