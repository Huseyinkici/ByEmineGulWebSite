using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using WebSite.Data.Models;
using WebSite.Repositories;
using X.PagedList.Extensions;

namespace WebSite.Controllers
{
    public class ProductImageController : Controller
    {
        ProductImageRepository productImageRepository = new ProductImageRepository();
        Context c = new Context();
        public IActionResult Index(string query, int page = 1)
        {
            // Fetch product images and apply search filter if query is provided
            var productImages = productImageRepository.TList("Product").AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                productImages = productImages.Where(p => p.Product.ProductName.Contains(query));
                ViewBag.Query = query; // Retain the query in the input field
            }

            return View(productImages.ToPagedList(page, 5));
        }
        [HttpGet]
        public IActionResult AddProductImage()
        {
            List<SelectListItem> values1 = (from x in c.products.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.ProductName,
                                               Value = x.ProductID.ToString()
                                           }).ToList();
            ViewBag.v2 = values1;
            return View();
        }
        [HttpPost]
        public IActionResult AddProductImage(ProductImageAdd p)
        {
            if (p.ProductImageURL != null && p.ProductImageURL.Count > 0)
            {
                foreach (var file in p.ProductImageURL)
                {
                    if (file.Length > 0) // Boş dosya kontrolü
                    {
                        // Dosya uzantısını al
                        var extension = Path.GetExtension(file.FileName);

                        // Benzersiz bir dosya adı oluştur
                        var newImageName = Guid.NewGuid() + extension;

                        // Yükleme hedefi
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "public_html", "images", "resimler1", newImageName);

                        // Dosyayı sunucuya kaydet
                        using (var stream = new FileStream(location, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        // Veritabanına kaydetmek için yeni bir ProductImage nesnesi oluştur
                        var pr = new ProductImage
                        {
                            ProductImageURL = Path.Combine("/public_html/images/resimler1", newImageName),
                            ProductID = p.ProductID
                        };

                        // Veritabanına ekle
                        productImageRepository.TAdd(pr);
                    }
                }
            }

            return RedirectToAction("Index");
        }


        public IActionResult DeleteProductImage(int id)
        {
            productImageRepository.TDelete(new ProductImage { ProductImageID = id });
            return RedirectToAction("Index");
        }
        public IActionResult GetProductImage(int id)
        {
            var x = productImageRepository.TGet(id);
            List<SelectListItem> values1 = (from t in c.products.ToList()
                                            select new SelectListItem
                                            {
                                                Text = t.ProductName,
                                                Value = t.ProductID.ToString()
                                            }).ToList();
            ViewBag.v2 = values1;
            ProductImage productImage = new ProductImage()
            {
                ProductImageID = x.ProductImageID,
                ProductID = x.ProductID,
                ProductImageURL = x.ProductImageURL
            };
            return View(productImage);
        }
        [HttpPost]
        public IActionResult UpdateProductImage(ProductImage p)
        {
            var x = productImageRepository.TGet(p.ProductImageID);
            x.ProductID = p.ProductID;
            x.ProductImageURL = p.ProductImageURL;
            x.ProductID = p.ProductID;
            productImageRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
    }
}