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
       
    }
}
