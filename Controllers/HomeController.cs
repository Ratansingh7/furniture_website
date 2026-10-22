using Microsoft.AspNetCore.Mvc;
using WoodCraftFurniture.Data;
using WoodCraftFurniture.Models;

namespace WoodCraftFurniture.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var featuredProducts = _context.Products.Where(p => p.IsFeatured).Take(6).ToList();
            return View(featuredProducts);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
