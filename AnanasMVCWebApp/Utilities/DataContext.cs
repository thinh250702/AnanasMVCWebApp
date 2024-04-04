using AnanasMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnanasMVCWebApp.Utilities {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductSKU> ProductSKUs { get; set; }
        /*public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShippingInfo> ShippingInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<OrderDetail>().HasKey(t => new { t.ProductSKUId, t.OrderId });
            modelBuilder.Entity<ShippingInfo>().HasNoKey();
        }*/

    }
}
