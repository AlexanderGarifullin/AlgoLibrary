using AlgoLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Data;

namespace AlgoLibrary.Controllers
{
    public class ThemeController : Controller
    {
        private readonly AppDbContext _context;
        public ThemeController(AppDbContext context)
        {
            _context = context;

        }


        public IActionResult Themes()
        {

            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }

            List<ThemeModel> themeModels = _context.Theme.OrderBy(t => t.OrderNumber).ToList();
            return View(themeModels);
        }

        public IActionResult Create()
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            var model = new ThemeModel();
            return View("ThemeChange", model); 
        }

        public IActionResult Edit(int id)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            var model = _context.Theme.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View("ThemeChange", model);
        }

        [HttpPost]
        public IActionResult ThemeChange(ThemeModel themeModel)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            themeModel.Name = themeModel.Name.Trim();

            int id = themeModel.ThemeId;
            string name = themeModel.Name;
            int orderNumber = themeModel.OrderNumber;

            if (!CheckThemeData(name)) 
            {
                ViewData["ErrorMessage"] = StringConstant.ThemeInputError;
                if (id == 0)
                {
                    return View("ThemeChange", new ThemeModel());
                } else
                {
                    return View("ThemeChange", themeModel);
                }
            }

            if (id == 0)
            {
                // add
                try
                {
                    themeModel.OrderNumber = 0;
                    _context.Theme.Add(themeModel);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Themes));
                }
                catch (Exception e)
                {
                    return View("ThemeChange", new ThemeModel());
                }
            }
            else
            {
                // edit
                try
                {
                    var existingTheme = _context.Theme.FirstOrDefault(u => u.ThemeId == id);
                    if (existingTheme == null)
                    {
                        return NotFound();
                    }
                    existingTheme.Name = name;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Themes));
                }
                catch (Exception e)
                {
                    return View("ThemeChange", new ThemeModel());
                }
            }
        }

        public IActionResult Delete(int id)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            var themeToDelete = _context.Theme.Find(id);

            if (themeToDelete == null)
            {
                return NotFound();
            }

            _context.Theme.Remove(themeToDelete);
            _context.SaveChanges();

            return RedirectToAction("Themes");
        }

        [HttpPost]
        public IActionResult SaveOrder(List<int> themeIds)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            try
            {

                List<ThemeModel> sortedThemes = themeIds
                    .Select(id => _context.Theme.FirstOrDefault(theme => theme.ThemeId == id))
                    .Where(theme => theme != null)
                    .ToList();

                for (int i = 0; i < sortedThemes.Count; i++)
                {
                    sortedThemes[i].OrderNumber = i + 1;
                }
                _context.SaveChanges();
                return Ok("Порядок сохранен успешно!");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка сохранения порядка: " + ex.Message);
            }
        }

        private bool CheckThemeData(string name)
        {
            if (name.Length > 50) return false;
            return true;
        }
    }
}
