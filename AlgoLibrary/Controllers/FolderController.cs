using AlgoLibrary.Models;
using Microsoft.AspNetCore.Mvc;

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
            List<FolderModel> folderModels = _context.Folder.OrderBy(t => t.OrderNumber).ToList();
            return View(folderModels);
        }

        public IActionResult Create()
        {
            var model = new FolderModel();
            return View("FolderChange", model);
        }

        public IActionResult Edit(int id)
        {
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
            int id = folderModel.FolderId;
            string name = folderModel.Name;
            int orderNumber = folderModel.OrderNumber;
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
            var folderToDelete = _context.Folder.Find(id);

            if (folderToDelete == null)
            {
                return NotFound();
            }

            _context.Folder.Remove(folderToDelete);
            _context.SaveChanges();

            return RedirectToAction("Folders");
        }
    }
}
