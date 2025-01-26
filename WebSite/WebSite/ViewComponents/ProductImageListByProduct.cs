using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebSite.Repositories;

namespace WebSite.ViewComponents
{
    public class ProductImageListByProduct : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
        ProductImageRepository productImageRepository = new ProductImageRepository();
        var productimagelist = productImageRepository.List(x=>x.ProductID == id);
        return View(productimagelist);
    }
}
}