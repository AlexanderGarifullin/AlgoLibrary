using AlgoLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlgoLibrary.Controllers
{
    public class AuthorisationController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorisationController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Authorisation(string msg)
        {
            ViewData["ErrorMessage"] = msg;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {  
            var user = _context.User.FirstOrDefault(u => u.Login == login && u.Password == Hashing.EncryptPassword(password));
            if (user != null)
            {
                DeleteSessionParameters();

                SessionParameters.UserName = user.Login;
                SessionParameters.UserId = user.UserId;
                UserRole role = UserRole.User;
                if (user.Role == "Admin") role = UserRole.Admin;
                else if (user.Role == "Moderator") role = UserRole.Moderator;
                SessionParameters.UserRoot = role;

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Authorisation", new { msg = StringConstant.AuthorisationError });
        }
        private void DeleteSessionParameters()
        {
            SessionParameters.UserName = "";
            SessionParameters.UserId = -1;
            SessionParameters.UserRoot = UserRole.User;

        }

        [HttpPost]
        public IActionResult Logout()
        {
            DeleteSessionParameters();
            return RedirectToAction("Authorisation");
        }

    }
}
