using AlgoLibrary.Models;
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
            ViewData["ThemeId"] = themeId;
            return View(articles);
        }

        public IActionResult Create(int themeId)
        {
            var model = new ArticleModel();
            model.ThemeId = themeId;
            return View("ArticleChange", model);
        }

        public IActionResult Edit(int id)
        {
            var model = _context.Article.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View("ArticleChange", model);
        }

        [HttpPost]
        public IActionResult CreateArticle(ArticleModel articleModel)
        {
            try
            {
                _context.Article.Add(articleModel);
                _context.SaveChanges();
                return RedirectToAction("Articles", new { themeId = articleModel.ThemeId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при сохранении конспекта: " + ex.Message);
            }
        }
    }
}
