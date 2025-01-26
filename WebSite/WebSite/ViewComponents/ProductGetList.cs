using Microsoft.AspNetCore.Mvc;
using WebSite.Repositories;

namespace WebSite.ViewComponents
{
    public class ProductGetList : ViewComponent
    {
        public IViewComponentResult Invoke(int limit = int.MaxValue)
        {
            ProductRepository productRepository = new ProductRepository();
            var productlist = productRepository.TList();
            var limitedproductlist = productlist.Take(limit).ToList();
            return View(limitedproductlist);
        }
    }
}

