using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using WebSite.Data.Models;
using WebSite.Repositories;
using X.PagedList.Extensions;

namespace WebSite.Controllers
{
    public class ProductController : Controller
    {
        Context c = new Context();
        ProductRepository productRepository = new ProductRepository();
        public IActionResult Index(string query, int page = 1)
        {
            var products = productRepository.TList("Category")
            .Where(p => string.IsNullOrEmpty(query) ||
                p.ProductName.Contains(query) ||
                p.ProductDescription.Contains(query))
            .ToPagedList(page, 5);


            ViewBag.Query = query; // Arama kutusuna geri yüklemek için
            return View("Index", products);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            List<SelectListItem> values = (from x in c.categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductAdd p)
        {
            Product pr = new Product();
            if (p.ImageURL != null)
            {
                var extension = Path.GetExtension(p.ImageURL.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "public_html", "images", "resimler", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageURL.CopyTo(stream);
                pr.ImageURL = newImageName;
            }
            pr.ProductName = p.ProductName;
            pr.Price = p.Price;
            pr.CategoryID = p.CategoryID;
            pr.ProductDescription = p.ProductDescription;
            productRepository.TAdd(pr);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteProduct(int id)
        {
            productRepository.TDelete(new Product { ProductID = id });
            return RedirectToAction("Index");
        }
        public IActionResult ProductGet(int id)
        {
            var x = productRepository.TGet(id);
            List<SelectListItem> values = (from y in c.categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.CategoryName,
                                               Value = y.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            Product f = new Product()
            {
                ProductID = x.ProductID,
                CategoryID = x.CategoryID,
                ProductName = x.ProductName,
                Price = x.Price,
                ProductDescription = x.ProductDescription,
                ImageURL = x.ImageURL
            };
            return View(f);
        }
        [HttpPost]
        public IActionResult ProductUpdate(Product p)
        {
            var x = productRepository.TGet(p.ProductID);
            x.ProductName = p.ProductName;
            x.Price = p.Price;
            x.ProductDescription = p.ProductDescription;
            x.ImageURL = p.ImageURL;
            x.CategoryID = p.CategoryID;
            productRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
    }
}