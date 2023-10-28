using first.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace first.Models
{
    public class Context:DbContext
    {

        public Context()
        {
        }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseSqlServer("Data Source=.;Initial Catalog=Nada;Integrated Security=True");//dbms , server name , db, autha-windows
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // the one-to-many relationship between User and Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            // the one-to-many relationship between Order and OrderDetails
            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Order)
                .WithMany(u => u.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            // the one-to-many relationship between product and OrderDetails
            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Product)
                .WithMany(u => u.OrderDetails)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
          
            // the one-to-many relationship between product and Review
            modelBuilder.Entity<Review>()
                .HasOne(o => o.Product)
                .WithMany(u => u.Reviews)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            // the one-to-many relationship between user and Review
            modelBuilder.Entity<Review>()
                .HasOne(o => o.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            //1:n category: product
            modelBuilder.Entity<Product>()
               .HasOne(c => c.Category)
               .WithMany(b => b.Products)
               .HasForeignKey(cat => cat.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            // the one-to-many relationship between Admin and Product
            modelBuilder.Entity<Product>()
                .HasOne(a => a.Admin)
                .WithMany(p => p.Products)
                .HasForeignKey(a => a.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
            // the one-to-many relationship between Admin and category
            modelBuilder.Entity<Category>()
             .HasOne(a=>a.Admin)
             .WithMany(c=>c.Categories)
             .HasForeignKey(a=>a.AdminId)
             .OnDelete(DeleteBehavior.Restrict);



        }

    }
}