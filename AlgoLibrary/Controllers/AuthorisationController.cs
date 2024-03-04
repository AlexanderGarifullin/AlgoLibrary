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
    }
}
