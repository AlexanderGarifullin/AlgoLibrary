using Microsoft.AspNetCore.Mvc;

namespace AlgoLibrary.Controllers
{
    public class ArticleController : Controller
    {

        private readonly AppDbContext _context;
        public ArticleController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Articles(int themeId)
        {
            var theme = _context.Theme.FirstOrDefault(t => t.ThemeId == themeId);
            if (theme == null)
            {
                return NotFound();
            }

            var articles = _context.Article.Where(a => a.ThemeId == themeId).ToList();

            ViewData["ThemeName"] = theme.Name;
            return View(articles);
        }
    }
}
