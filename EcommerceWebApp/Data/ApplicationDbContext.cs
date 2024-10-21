using EcommerceWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EcommerceWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
         
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mobiles", DisplayOrder = 1 },
                new Category { Id = 2, Name = "TVs", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Laptop", DisplayOrder = 3 }
            );

			modelBuilder.Entity<Product>()
			.HasOne(p => p.Category)
			.WithMany(c => c.Products)
			.HasForeignKey(p => p.CategoryId);

			modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Samsung Galaxy S24", Description = "Samsung Galaxy S24, 8GB RAM, 256GB ROM, 48MP Camera", Price = 80000, CategoryId = 1, ImageUrl=""},
                new Product { Id = 2, Name = "Samsung Galaxy S23", Description = "Samsung Galaxy S23, 8GB RAM, 128GB ROM, 32MP Camera", Price = 70000, CategoryId = 1, ImageUrl = "" },
                new Product { Id = 3, Name = "Apple iPhone 15", Description = "Apple iPhone 15, 6GB RAM, 128GB ROM, 32MP Camera", Price=100000, CategoryId = 1, ImageUrl = "" }
            );
        }
    }
}
