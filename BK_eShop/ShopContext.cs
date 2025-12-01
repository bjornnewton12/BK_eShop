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
            });

            modelBuilder.Entity<OrderRow>(r =>
            {
                // PK
                r.HasKey(x => x.OrderRowId);

                // Properties
                r.Property(x => x.OrderRowQuantity).IsRequired();
            });

            modelBuilder.Entity<Product>(p =>
            {
                // PK
                p.HasKey(x => x.ProductId);

                // Properties
                p.Property(x => x.ProductName).IsRequired().HasMaxLength(150);
                p.Property(x => x.ProductPrice).IsRequired();
                p.Property(x => x.ProductStock).IsRequired();
            });


            /*// Order Summary
            modelBuilder.Entity<OrderSummary>(e =>
            {
                e.HasNoKey(); // Saknar PK
                e.ToView("OrderSummaryView"); // Kopplar tabellen mot SQL
            });*/
        }
    }
}
