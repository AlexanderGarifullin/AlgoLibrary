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

        public IActionResult Authorisation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {

            var user = _context.User.FirstOrDefault(u => u.Login == login && u.Password == Hashing.EncryptPassword(password));
            if (user != null)
            {

                return RedirectToAction("Index", "Home");
            }
            return View("Authorisation");
        }
    }
}
