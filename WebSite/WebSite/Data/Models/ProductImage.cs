namespace WebSite.Data.Models
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        public int ProductID { get; set; }
        public string ProductImageURL { get; set; }
        public virtual Product Product { get; set; }
    }
}