using bageri.api.Entities;
using bageri2.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace bageri.api.Data; 

    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers  { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderItem> OrderItems { get; set; }
   

    public DataContext(DbContextOptions options) : base(options)
       {
       }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierProduct>().HasKey(i => new {i.SupplierId, i.ProductId});
        modelBuilder.Entity<OrderItem>().HasKey(i => new {i.ProductId, i.OrderId});
    }
}
