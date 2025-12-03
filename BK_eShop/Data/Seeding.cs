using Microsoft.EntityFrameworkCore;
using BK_eShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Data
{
    public static class Seeding
    {
        public static async Task SeedAsync()
        {
            using var db = new ShopContext();

            await db.Database.MigrateAsync();

            if (!await db.Customers.AnyAsync())
            {
                db.Customers.AddRange(
                    new Customer { CustomerName = "Horowitz, Cher", CustomerPhone = 0739124318, CustomerEmail = "cher@me.com" },
                    new Customer { CustomerName = "Davenport, Dionne", CustomerPhone = 0742670934, CustomerEmail = "dd@me.com" }
                );
                await db.SaveChangesAsync();
                Console.WriteLine("Seeded customers db");
            }

            if (!await db.Categories.AnyAsync())
            {
                db.Categories.AddRange(
                    new Category { CategoryName = "Paintings" },
                    new Category { CategoryName = "Shoes" }
                );
            }

            if (!await db.Products.AnyAsync())
            {
                db.Products.AddRange(
                    new Product { ProductName = "Monét painting", ProductPrice = 45000M, ProductStock = 5 },
                    new Product { ProductName = "Louboutins", ProductPrice = 900M, ProductStock = 100 }
                );
            }

        }
    }
}
