using Microsoft.AspNetCore.Mvc;
using WoodCraftFurniture.Data;
using WoodCraftFurniture.Models;

namespace WoodCraftFurniture.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now;
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool IsAdmin()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            return isAdmin == "True";
        }
    }
}
