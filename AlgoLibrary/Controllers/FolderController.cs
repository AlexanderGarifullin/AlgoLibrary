﻿using AlgoLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using static NuGet.Packaging.PackagingConstants;

namespace AlgoLibrary.Controllers
{
    public class FolderController : Controller
    {
        private readonly AppDbContext _context;
        public FolderController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Folders()
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            List<FolderModel> folderModels = _context.Folder.OrderBy(t => t.OrderNumber).ToList();
            return View(folderModels);
        }

        public IActionResult Create()
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            var model = new FolderModel();
            return View("FolderChange", model);
        }

        public IActionResult Edit(int id)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            var model = _context.Folder.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View("FolderChange", model);
        }


        [HttpPost]
        public IActionResult FolderChange(FolderModel folderModel)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            folderModel.Name = folderModel.Name.Trim();
            int id = folderModel.FolderId;
            string name = folderModel.Name;
            int orderNumber = folderModel.OrderNumber;
            if (!CheckFolderData(name)) {
                ViewData["ErrorMessage"] = StringConstant.FolderInputError;
                if (id == 0) folderModel = new FolderModel();
                return View("FolderChange", folderModel);
            }
            if (id == 0)
            {
                // add
                try
                {
                    folderModel.OrderNumber = 0;
                    _context.Folder.Add(folderModel);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Folders));
                }
                catch (Exception e)
                {
                    return View("FolderChange", new FolderModel());
                }
            }
            else
            {
                // edit
                try
                {
                    var existingFolder = _context.Folder.FirstOrDefault(u => u.FolderId == id);
                    if (existingFolder == null)
                    {
                        return NotFound();
                    }
                    existingFolder.Name = name;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Folders));
                }
                catch (Exception e)
                {
                    return View("FolderChange", new FolderModel());
                }
            }
        }


        public IActionResult Delete(int id)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            var folderToDelete = _context.Folder.Find(id);

            if (folderToDelete == null)
            {
                return NotFound();
            }

            _context.Folder.Remove(folderToDelete);
            _context.SaveChanges();

            return RedirectToAction("Folders");
        }

        [HttpPost]
        public IActionResult SaveOrder(List<int> folderIds)
        {
            if (SessionParameters.UserRoot == UserRole.User)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            try
            {

                List<FolderModel> sortedFolders = folderIds
                    .Select(id => _context.Folder.FirstOrDefault(folder => folder.FolderId == id))
                    .Where(folder => folder != null)
                    .ToList();

                for (int i = 0; i < sortedFolders.Count; i++)
                {
                    sortedFolders[i].OrderNumber = i + 1;
                }
                _context.SaveChanges();
                return Ok("Порядок сохранен успешно!");
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка сохранения порядка: " + ex.Message);
            }
        }

        private bool CheckFolderData(string name)
        {
            if (name.Length > 50) return false;
            return true;
        }
    }
}
