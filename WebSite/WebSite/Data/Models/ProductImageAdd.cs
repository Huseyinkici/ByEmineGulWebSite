using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebSite.Data.Models
{
    public class ProductImageAdd
    {
        public List<IFormFile> ProductImageURL { get; set; }
        public int ProductID { get; set; }
    }
}
