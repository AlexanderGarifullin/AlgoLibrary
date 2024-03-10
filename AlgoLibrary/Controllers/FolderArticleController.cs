using Microsoft.AspNetCore.Mvc;
using static NuGet.Packaging.PackagingConstants;

namespace AlgoLibrary.Controllers
{
    public class FolderArticleController : Controller
    {
        private readonly AppDbContext _context;
        private int currentFolderId = 0;
        public FolderArticleController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult FolderArticle(int folderId)
        {
            var folder = _context.Folder.FirstOrDefault(t => t.FolderId == folderId);
            if (folder == null)
            {
                return NotFound();
            }

            var articlesInFolder = _context.Folder_Article
            .Where(fa => fa.FolderId == folderId)
            .OrderBy(fa => fa.OrderNumber)
            .Select(fa => fa.Article)
            .ToList();

            var allArticles = _context.Article
            .Where(a => !articlesInFolder.Contains(a))
            .OrderBy(a => a.Name)
            .ToList();

            ViewData["AvailableArticles"] = allArticles;
            ViewData["AddedArticles"] = articlesInFolder;
            ViewData["FolderName"] = folder.Name;
            currentFolderId = folderId;

            return View();
        }
    }
}
