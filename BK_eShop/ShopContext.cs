using BK_eShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop
{
    public class ShopContext : DbContext
    {
        // Gör alla DBSet
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderRow> OrderRows => Set<OrderRow>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<OrderSummary> OrderSummaries => Set<OrderSummary>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppContext.BaseDirectory, "shop.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(c =>
            {
                // PK
                c.HasKey(x => x.CustomerId);

                // Properties
                c.Property(x => x.CustomerName).IsRequired().HasMaxLength(150);
                c.Property(x => x.CustomerPhone).IsRequired();
                c.Property(x => x.CustomerEmail).IsRequired().HasMaxLength(150);
                c.Property(x => x.CustomerPassword).IsRequired();

                // Is unique
                c.HasIndex(x => x.CustomerEmail).IsUnique();
            });

            modelBuilder.Entity<Order>(o =>
            {
                // PK
                o.HasKey(x => x.OrderId);

                // Properties
                o.Property(x => x.OrderDate).IsRequired();
                o.Property(x => x.OrderStatus).IsRequired();
                o.Property(x => x.OrderTotalAmount).IsRequired();

                // Relationship Customer -> Order
                o.HasOne(x => x.Customer)
                    .WithMany(x => x.Orders)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderRow>(r =>
            {
                // PK
                r.HasKey(x => x.OrderRowId);

                // Properties
                r.Property(x => x.OrderRowQuantity).IsRequired();

                // Relationship Order -> OrderRow
                r.HasOne(x => x.Order)
                    .WithMany(x => x.OrderRows)
                    .HasForeignKey(x => x.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship Product -> OrderRow
                r.HasOne(x => x.Product)
                    .WithMany()
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(p =>
            {
                // PK
                p.HasKey(x => x.ProductId);

                // Properties
                p.Property(x => x.ProductName).IsRequired().HasMaxLength(150);
                p.Property(x => x.ProductPrice).IsRequired();
                p.Property(x => x.ProductStock).IsRequired();

                // Is unique
                p.HasIndex(x => x.ProductName).IsUnique();

                // Relationship Category -> Product
                p.HasOne(x => x.Categories)
                        .WithMany(x => x.Products)
                        .HasForeignKey(x => x.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(t =>
            {
                // PK
                t.HasKey(x => x.CategoryId);

                // Properties
                t.Property(x => x.CategoryName).IsRequired();

                // Is unique
                t.HasIndex(x => x.CategoryName).IsUnique();                
            });

            // Index used for Order list
            // modelBuilder.Entity<Order>().HasIndex(o => o.OrderDate);
            // modelBuilder.Entity<Order>().HasIndex(o => o.CustomerId);

            // Index used for Product list
            // modelBuilder.Entity<Product>().HasIndex(p => p.ProductId);
            // modelBuilder.Entity<Product>().HasIndex(p => p.CategoryId);
        }
    }
}
