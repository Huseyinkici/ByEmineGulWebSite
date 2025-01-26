using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSite.Data.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(255)] // Maksimum uzunluğu belirtiyoruz
        public string CategoryName { get; set; }

        [Required]
        public string CategoryDescription { get; set; } // TEXT veri türü için uygun

        public List<Product> Products { get; set; }
    }
}