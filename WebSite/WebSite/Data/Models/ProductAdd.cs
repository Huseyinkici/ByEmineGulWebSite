using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebSite.Data.Models
{
    public class ProductAdd
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(255, ErrorMessage = "Product name cannot exceed 255 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile ImageURL { get; set; } // Resim dosyası için gerekli

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative value.")]
        public int UrunKod { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryID { get; set; }
    }
}