using Microsoft.AspNetCore.Mvc;
using WebSite.Repositories;

namespace WebSite.ViewComponents
{
    public class ProductListByCategory : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {

            ProductRepository productRepository = new ProductRepository();
            var productlist = productRepository.List(x=>x.CategoryID==id);
            return View(productlist);
        }
    }
}