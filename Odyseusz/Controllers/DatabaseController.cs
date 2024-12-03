using Microsoft.AspNetCore.Mvc;

namespace Odyseusz.Controllers
{
    public class DatabaseController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Layout"] = "~/Views/Shared/_DatabaseLayout.cshtml";
            return View();
        }
    }
}
