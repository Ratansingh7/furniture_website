using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WoodCraftFurniture.Models;

namespace WoodCraftFurniture.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart";

        public IActionResult Index()
        {
            var cart = GetCart();
            ViewBag.Total = cart.Sum(item => item.Total);
            return View(cart);
        }

        public IActionResult AddToCart(int productId, string productName, string imageUrl, decimal price, int quantity = 1)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    ImageUrl = imageUrl,
                    Price = price,
                    Quantity = quantity
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;
            }
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        private List<CartItem> GetCart()
        {
            var sessionData = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(sessionData))
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(sessionData) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            var sessionData = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(CartSessionKey, sessionData);
        }
    }
}
