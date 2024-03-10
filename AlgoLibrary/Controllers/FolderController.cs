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

    }
}
