using Microsoft.EntityFrameworkCore;

namespace WebSite.Data.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=DbCoreDress;user=root;password=213533;",
                new MySqlServerVersion(new Version(8, 0, 21))); // MySQL sürümünü güncelleyin
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<ProductImage> productimages { get; set; }
    }
}