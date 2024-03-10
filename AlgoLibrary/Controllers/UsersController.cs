using Microsoft.AspNetCore.Mvc;
using AlgoLibrary.Models;
using Humanizer.Localisation;

namespace AlgoLibrary.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Users(string msg)
        {
            if (SessionParameters.UserRoot != UserRole.Admin)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            ViewData["ErrorMessage"] = msg;
            List<UserModel> users = _context.User.ToList();
            return View(users);
        }
        public IActionResult Delete(int id)
        {
            if (SessionParameters.UserRoot != UserRole.Admin)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            if (id == SessionParameters.UserId)  
            {
                return RedirectToAction("Users", new { msg = StringConstant.DeleteHimselfError});
            }
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
            if (SessionParameters.UserRoot != UserRole.Admin)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            return View("Change", new UserModel());
        }
        public IActionResult Edit(int id)
        {
            if (SessionParameters.UserRoot != UserRole.Admin)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
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
            if (SessionParameters.UserRoot != UserRole.Admin)
            {
                return View("~/Views/Users/Rights.cshtml");
            }
            int id = userModel.UserId;
            string login = userModel.Login.Trim();
            string password = userModel.Password.Trim();
            string role = userModel.Role;
            if (!IsCorrectUserData(id, login, password, role))
            {
                ViewData["ErrorMessage"] = StringConstant.UsersInputError;
                if (id == 0) return View("Change", new UserModel()); 
                else
                {
                    return View("Change", userModel);
                }
            }
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
                    ViewData["ErrorMessage"] = StringConstant.DbError;
                    return View("Change", new UserModel());
                }              
            } else
            {
                // edit
                try
                {
                    if (id == SessionParameters.UserId) RedirectToAction(nameof(Users));
                    var existingUser = _context.User.FirstOrDefault(u => u.UserId == id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }
                    existingUser.Login = login;
                    existingUser.Password = Hashing.EncryptPassword(password);
                    existingUser.Role = role;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Users));
                }
                catch (Exception e)
                {
                    ViewData["ErrorMessage"] = StringConstant.DbError;
                    return View("Change", new UserModel());
                }
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
