﻿using AlgoLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            var theme = _context.Theme.FirstOrDefault(t => t.ThemeId == themeId);
            if (theme == null)
            {
                return NotFound();
            }

            var articles = _context.Article
                .Where(a => a.ThemeId == themeId)
                .OrderBy(a => a.OrderNumber)
                .ToList();

            ViewData["ThemeName"] = theme.Name;
            ViewData["ThemeId"] = themeId;
            return View(articles);
        }

        public IActionResult Create(int themeId)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            var model = new ArticleModel();
            model.ThemeId = themeId;
            return View("ArticleChange", model);
        }

        public IActionResult Edit(int id)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
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
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            articleModel.Name = articleModel.Name.Trim();

            if (!CheckArticleData(articleModel.Name))
            {
                ViewData["ErrorMessage"] = StringConstant.ArticleInputError;
                return View("ArticleChange", articleModel);
            }
            if (articleModel.ArticleId == 0)
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
            } else
            {
                try
                {
                    var existingArticle = _context.Article.FirstOrDefault(u => u.ArticleId == articleModel.ArticleId);
                    if (existingArticle == null)
                    {
                        return NotFound();
                    }
                    existingArticle.Text = articleModel.Text;
                    existingArticle.Name = articleModel.Name;
                    _context.SaveChanges();
                    return RedirectToAction("Articles", new { themeId = articleModel.ThemeId });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Произошла ошибка при сохранении конспекта: " + ex.Message);
                }
            }
        }

        public IActionResult Delete(int id)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            try
            {
                var article = _context.Article.Find(id);
                if (article == null)
                {
                    return NotFound();
                }

                _context.Article.Remove(article);
                _context.SaveChanges();

                return RedirectToAction("Articles", new { themeId = article.ThemeId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при удалении конспекта: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SaveOrder(List<int> articlesIds)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            try
            {

                List<ArticleModel> sortedArticles = articlesIds
                    .Select(id => _context.Article.FirstOrDefault(article => article.ArticleId == id))
                    .Where(article => article != null)
                    .ToList();

                for (int i = 0; i < sortedArticles.Count; i++)
                {
                    sortedArticles[i].OrderNumber = i + 1;
                }
                _context.SaveChanges();
                return Ok("Порядок сохранен успешно!");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка сохранения порядка: " + ex.Message);
            }
        }


        private bool CheckArticleData(string name)
        {
            if (name.Length > 50) return false;
            return true;
        }

    }
}
