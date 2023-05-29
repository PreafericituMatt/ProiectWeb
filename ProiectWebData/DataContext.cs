using Microsoft.EntityFrameworkCore;
using ProiectWeb.Entities;
using ProiectWebData.Entities;

namespace ProiectWebData
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Items> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasOne(od => od.BillingAddress).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderDetails>().HasOne(od => od.DeliveryAddress).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}