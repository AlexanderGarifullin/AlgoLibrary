using Microsoft.AspNetCore.Mvc;

namespace AlgoLibrary.Controllers
{
    public class PlanController : Controller
    {
        private readonly AppDbContext _context;
        public PlanController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Plan()
        {
            return View();
        }
    }
}
