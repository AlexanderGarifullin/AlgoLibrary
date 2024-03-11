using AlgoLibrary.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AlgoLibrary.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult Index(int articleId = 0)
        {
            string markdownContent = @"# Добро пожаловать в Algomaster!

Приветствуем вас на Algomaster - вашем источнике знаний о алгоритмах с возможностью закрепления на практике!

Algomaster - это библиотека алгоритмов, разработанная для помощи как начинающим, так и опытным разработчикам в изучении и понимании алгоритмов. Мы верим, что понимание алгоритмов является ключом к успешной карьере в сфере программирования.

## Почему Algomaster?

### Обширная библиотека алгоритмов

Algomaster предлагает широкий спектр алгоритмов из различных областей информатики, таких как сортировка, поиск, структуры данных, алгоритмы на графах и многое другое. Вы найдете здесь как классические, так и современные алгоритмы.

### Практическое применение

В конце каждой статьи вы найдете ссылки на задачи на платформе Codeforces, которые помогут вам закрепить изученные материалы на практике. Чтобы начать решать задачу нужно вступить в [группу](https://codeforces.com/group/eYghlTf5Xb/contests).

### План изучения для новичков

Для новичков мы разработали специальный план изучения, который поможет вам структурировать ваш путь обучения алгоритмам. Начните с основ и постепенно переходите к более сложным концепциям.

## Начните сейчас!

Не откладывайте ваше обучение на потом. Присоединяйтесь к Algomaster прямо сейчас и начните углублять свои знания об алгоритмах!

С уважением,
Команда Algomaster
";
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

            var articlesWithThemes = _context.Article.Include(a => a.Theme).ToList();
            ViewBag.ArticlesWithThemes = articlesWithThemes;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult HomeRedirection()
        {
            return View();
        }
    }
}