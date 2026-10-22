using Microsoft.AspNetCore.Mvc;
using WoodCraftFurniture.Data;
using WoodCraftFurniture.Models;

namespace WoodCraftFurniture.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string category, string searchString)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
                ViewBag.CurrentCategory = category;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString));
                ViewBag.SearchString = searchString;
            }

            return View(products.ToList());
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
