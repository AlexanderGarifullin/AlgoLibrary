using Microsoft.AspNetCore.Mvc;

namespace AlgoLibrary.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Articles()
        {
            return View();
        }
    }
}
