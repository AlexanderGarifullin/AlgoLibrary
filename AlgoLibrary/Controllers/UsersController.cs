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

            return RedirectToAction("Users");
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

        [HttpPost]
        public IActionResult Create(UserModel userModel)
        {
            int id = userModel.UserId;
            string login = userModel.Login;
            string password = userModel.Password;
            string role = userModel.Role;
            if (!IsCorrectUserData(id, login, password, role)) return View("Change", new UserModel());
            if (id == 0)
            {
                // add
                try
                {
                    userModel.Password = Hashing.EncryptPassword(password);
                    _context.User.Add(userModel);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Users));
                }
                catch (Exception e)
                {
                    return View("Change", new UserModel());
                }              
            } else
            {
                // edit
                return RedirectToAction(nameof(Users));
            }
        }

        private bool IsCorrectUserData(int id, string login, string password, string role)
        {
            if (login.Length > 40) return false;
            if (password.Length > 256) return false;
            return true;
        }
    }
}
