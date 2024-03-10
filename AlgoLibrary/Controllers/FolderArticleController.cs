using AlgoLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using static NuGet.Packaging.PackagingConstants;

namespace AlgoLibrary.Controllers
{
    public class FolderArticleController : Controller
    {
        private readonly AppDbContext _context;

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
            ViewData["FolderId"] = folderId;
            return View();
        }

        [HttpPost]
        public IActionResult SaveOrder(List<int> articlesIds, int currentFolderId)
        {
            try
            {

                var folderArticles = _context.Folder_Article
                .Where(fa => fa.FolderId == currentFolderId && articlesIds.Contains(fa.ArticleId))
                .ToList();

                folderArticles.Sort((a, b) => articlesIds.IndexOf(a.ArticleId).CompareTo(articlesIds.IndexOf(b.ArticleId)));


                for (int i = 0; i < folderArticles.Count; i++)
                {
                    folderArticles[i].OrderNumber = i + 1;
                }
                _context.SaveChanges();
                return Ok("Порядок сохранен успешно!");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка сохранения порядка: " + ex.Message);
            }
        }

        public IActionResult Add(int id, int folderId)
        {
            try
            {
                var folderArticle = new Folder_ArticleModel
                {
                    FolderId = folderId,
                    ArticleId = id,
                    OrderNumber = 0
                };

                _context.Folder_Article.Add(folderArticle);
                _context.SaveChanges();


                return RedirectToAction("FolderArticle", new { folderId = folderId });
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при добавлении в базу данных: " + ex.Message);
            }
        }

        public IActionResult Delete(int id, int folderId)
        {
            try
            {
                var folderArticle = _context.Folder_Article
                    .FirstOrDefault(fa => fa.ArticleId == id && fa.FolderId == folderId);

                if (folderArticle == null)
                {
                    return NotFound(); 
                }

                _context.Folder_Article.Remove(folderArticle);
                _context.SaveChanges();


                return RedirectToAction("FolderArticle", new { folderId = folderId });
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при удалении из базы данных: " + ex.Message);
            }
        }


    }
}
