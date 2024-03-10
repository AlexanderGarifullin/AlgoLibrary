using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoLibrary.Controllers
{
    public class PlanController : Controller
    {
        private readonly AppDbContext _context;
        public PlanController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Plan(int articleId = 0)
        {
            string markdownContent = @"# План обучения
Изучать алгоритмы непоследовательно, ориентируюсь лишь на темы, сложно, поэтому мы разработали специальный план обучения, который поможет вам справиться с этой задачей. В этом разделе конспекты разделены по уровням сложности. Начинайте свой путь с 1 уровня и постепенно дойдите до последнего!";
            string articleTitle = "Algomaster";
            if (articleId != 0)
            {
                var artical = _context.Article.FirstOrDefault(art => art.ArticleId == articleId);
                if (artical != null)
                {
                    articleTitle = artical.Name;
                    markdownContent = artical.Text;
                }
            }
            string htmlContent = Markdown.ToHtml(markdownContent, new MarkdownPipelineBuilder().UseMathematics().Build());
            ViewBag.MarkdownContent = htmlContent;
            ViewBag.ArticleTitle = articleTitle;

            var folderArticles = _context.Folder_Article
        .Include(fa => fa.Folder)
        .Include(fa => fa.Article)
        .OrderBy(fa => fa.Folder.OrderNumber)
        .ThenBy(fa => fa.OrderNumber)
        .ToList();
            ViewBag.FolderArticles = folderArticles;
            return View();
        }
    }
}
