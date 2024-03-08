using AlgoLibrary.Models;
using Microsoft.AspNetCore.Mvc;
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
            List<ThemeModel> themeModels = _context.Theme.ToList();
            return View(themeModels);
        }

        public IActionResult Create()
        {      
            var model = new ThemeModel();
            return View("ThemeChange", model); 
        }

        public IActionResult Edit(int id)
        {
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
            int id = themeModel.ThemeId;
            string name = themeModel.Name;
            int orderNumber = themeModel.OrderNumber;
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
            var themeToDelete = _context.Theme.Find(id);

            if (themeToDelete == null)
            {
                return NotFound();
            }

            _context.Theme.Remove(themeToDelete);
            _context.SaveChanges();

            return RedirectToAction("Themes");
        }
    }
}
