using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSite.Data;
using WebSite.Data.Models;

namespace WebSite.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index3()
        {
            return View();
        }
        public IActionResult VisualizeProductResult2()
        {
            return Json(ProductList());
        }
        public List<Class2> ProductList()
        {
            List<Class2> cs2 = new List<Class2>();
            using(var c = new Context())
            {
                cs2 = c.products.Select(x => new Class2()
                {
                    prodname = x.ProductName,
                }).ToList();
            }
            return cs2;
        }
    }
}
