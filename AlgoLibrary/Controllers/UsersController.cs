using Microsoft.AspNetCore.Mvc;
using AlgoLibrary.Models;

namespace AlgoLibrary.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Users()
        {
            if (SessionParameters.UserRoot != UserRole.Admin)
            {
                return View("Rights");
            }
            List<UserModel> users = _context.User.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var userToDelete = _context.User.Find(id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.User.Remove(userToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Users));
        }

        public IActionResult Create()
        {
            return View("Change", new UserModel());
        }
        public IActionResult Edit(int id)
        {
            UserModel user = _context.User.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View("Change", user);
        }
    }
}
