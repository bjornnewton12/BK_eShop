using BK_eShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Helpers
{
    public static class ProductHelper
    {
        // List products
        public static async Task ListProductsAsync()
        {
            using var db = new ShopContext();

            var allProducts = await db.Products.AsNoTracking()
                .Include(x => x.Category)
                .OrderBy(product => product.ProductId).ToListAsync();
            Console.WriteLine("Product Id | Product name | Category | Product price | Product stock");

            foreach (var allProduct in allProducts)
            {
                Console.WriteLine($"{allProduct.ProductId} | {allProduct.ProductName} | {allProduct.Category?.CategoryName} | {allProduct.ProductPrice} | {allProduct.ProductStock}");
            }
        }

        // Add product
        public static async Task AddProductAsync()
        {
            // Product name
            Console.Write("Product name: ");
            var productName = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(productName) || productName.Length > 150)
            {
                Console.WriteLine("Name is required and cannot be more than 150 characters");
                return;
            }

            await CategoryHelper.ListCategoriesAsync();
            // Product category
            Console.Write("Category: ");
            var category = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(category))
            {
                Console.WriteLine("Category is required");
            }

            // Product price
            Console.Write("Select price: ");
            var pPrice = Console.ReadLine();
            if (!int.TryParse(pPrice, out var productPrice))
            {
                Console.WriteLine("You need to input numbers");
                return;
            }

            // Product stock
            Console.Write("Select product stock: ");
            var pStock = Console.ReadLine();
            if (!int.TryParse(pStock, out var productStock))
            {
                Console.WriteLine("You need to input numbers");
                return;
            }

            using var db = new ShopContext();
            db.Products.Add(new Product { ProductName = productName, ProductPrice = productPrice, ProductStock = productStock });

            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("Product added");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Db Error: " + ex.GetBaseException().Message);
            }
        }
    }
}
