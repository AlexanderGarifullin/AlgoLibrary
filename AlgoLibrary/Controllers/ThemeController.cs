using Microsoft.AspNetCore.Mvc;

namespace AlgoLibrary.Controllers
{
    public class ThemeController : Controller
    {
        public IActionResult Themes()
        {
            return View();
        }
    }
}
