using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSite.Data.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(255)] // Ürün adı için maksimum uzunluk
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; } // TEXT veri türü için uygun

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public double Price { get; set; }

        public string ImageURL { get; set; }

        [ForeignKey("Category")] // Foreign key ilişkisini belirtir
        public int CategoryID { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }

        public virtual Category Category { get; set; } // Navigation property
    }
}