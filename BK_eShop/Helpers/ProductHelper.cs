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
                .Include(x => x.Categories)
                .OrderBy(product => product.ProductId).ToListAsync();
            Console.WriteLine("\nProduct Id | Product name | Category | Product price | Product stock");

            foreach (var allProduct in allProducts)
            {
                Console.WriteLine($"{allProduct.ProductId} | {allProduct.ProductName} | {allProduct.Categories?.CategoryName} | {allProduct.ProductPrice} | {allProduct.ProductStock}");
            }
        }

        // List products by category
        public static async Task ListProductsbyCategoryAsync()
        {
            using var db = new ShopContext();
            Console.WriteLine("\nPlease select a category to list its products");
            var categories = await db.Categories.AsNoTracking().ToListAsync();

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.CategoryId}: {category.CategoryName}");
            }

            if (!int.TryParse(Console.ReadLine(), out var idC))
            {
                Console.WriteLine("Category Id must be a number");
            }

            var categoryName = await db.Categories.FirstAsync(c => c.CategoryId == idC);
            var products = await db.Products.Where(c => c.CategoryId == idC).ToListAsync();

            Console.WriteLine($"\nProducts in the {categoryName.CategoryName} category");
            Console.WriteLine("Product Id | Product name | Product price | Product stock");

            foreach(var product in products)
            {
                Console.WriteLine($" {product.ProductId} | {product.ProductName} | {product.ProductPrice} | {product.ProductStock} ");
            }
        }

        // Add product
        public static async Task AddProductAsync()
        {
            // Product name
            Console.Write("\nProduct name: ");
            var productName = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(productName) || productName.Length > 150)
            {
                Console.WriteLine("Name is required and cannot be more than 150 characters");
                return;
            }
            Console.WriteLine("");
            await CategoryHelper.ListCategoriesAsync();

            // Product category
            Console.Write("\nSelect category Id: ");
            var categoryInput = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!int.TryParse(categoryInput, out var categoryId))
            {
                Console.WriteLine("Category Id must be a number");
                return;
            }

            using var db = new ShopContext();

            var category = await db.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found");
                return;
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

            var product = new Product
            {
                ProductName = productName,
                ProductPrice = productPrice,
                ProductStock = productStock,
                Categories = category
            };

            db.Products.Add(product);
            
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("\nProduct added");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Db Error: " + ex.GetBaseException().Message);
            }
        }

        // Delete product
        public static async Task DeleteProductAsync(int idDp)
        {
            using var db = new ShopContext();

            var product = await db.Products.FirstOrDefaultAsync(x => x.ProductId == idDp);
            if (product == null)
            {
                Console.WriteLine("Product not found");
                return;
            }

            db.Products.Remove(product);

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("\nProduct deleted");
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine("DB error: " + exception.GetBaseException().Message);
            }
        }
    }
}