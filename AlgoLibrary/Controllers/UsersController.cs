using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
    }
}
