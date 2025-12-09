using Microsoft.EntityFrameworkCore;
using BK_eShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BK_eShop.Helpers;

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
                    new Customer { CustomerName = "Horowitz, Cher", CustomerPhone = "0739124318", CustomerEmail = EncryptionHelper.Encrypt("cher@me.com"), CustomerPassword = EncryptionHelper.Encrypt("AsIf") },
                    new Customer { CustomerName = "Davenport, Dionne", CustomerPhone = "0742670934", CustomerEmail = EncryptionHelper.Encrypt("dd@me.com"), CustomerPassword = EncryptionHelper.Encrypt("woman") }
                );
                await db.SaveChangesAsync();
                Console.WriteLine("Seeded customers db");
            }

            if (!await db.Categories.AnyAsync())
            {
                db.Categories.AddRange(
                    new Category { CategoryName = "Paintings" },
                    new Category { CategoryName = "Blazers" }
                );
                await db.SaveChangesAsync();
                Console.WriteLine("Seeded categories db");
            }

            if (!await db.Products.AnyAsync())
            {
                var paintings = await db.Categories.FirstAsync(t => t.CategoryName == "Paintings");
                var shoes = await db.Categories.FirstAsync(t => t.CategoryName == "Blazers");

                db.Products.AddRange(
                    new Product { ProductName = "Monét painting", ProductPrice = 45000M, ProductStock = 5, CategoryId = 1 },
                    new Product { ProductName = "Plaid yellow blazer", ProductPrice = 900M, ProductStock = 100 , CategoryId = 2}
                );
                await db.SaveChangesAsync();
                Console.WriteLine("Seeded products db");
            }
        }
    }
}
