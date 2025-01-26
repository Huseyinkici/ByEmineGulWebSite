using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSite.Data.Models;

namespace WebSite.Controllers
{
    public class DefaultController : Controller
    {
        Context c = new Context();

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult CategoryDetails(int id)
        {
            var category = c.categories.FirstOrDefault(c => c.CategoryID == id);
            ViewBag.CategoryName = category.CategoryName;
            ViewBag.x = id;
            return View();
        }
        [AllowAnonymous]
        public IActionResult ProductDetails(int id)
        {
            // Ürünü veritabanından al
            var product = c.products.Include(p => p.ProductImages) // Ürün resimlerini de dahil et
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound(); // Ürün bulunamazsa 404 döndür
            }

            return View(product); // Ürün modelini view'a gönder
        }
        [AllowAnonymous]
        public IActionResult AllProducts()
        {
            var allProduct = c.products.Include(p => p.ProductImages).ToList();
            return View(allProduct);
        }
        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Shipping()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult SizeDetails()
        {
            return View();
        }
    }
}