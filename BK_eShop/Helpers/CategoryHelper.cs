using BK_eShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BK_eShop.Helpers
{
    public static class CategoryHelper
    {

        // List category
        public static async Task ListCategoriesAsync()
        {
            using var db = new ShopContext();

            var categories = await db.Categories.AsNoTracking().OrderBy(category => category.CategoryId).ToListAsync();
            Console.WriteLine("Category Id | Category Name");

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.CategoryId} | {category.CategoryName}"); 
            }

        }


        // Add category
        public static async Task AddCategoryAsync()
        {
            // Category name
            Console.Write("Category name: ");
            var categoryName = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(categoryName))
            {
                Console.WriteLine("Name is required");
                return;
            }

            using var db = new ShopContext();
            db.Categories.Add(new Category { CategoryName = categoryName });

            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("Category added");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Db Error: " + ex.GetBaseException().Message);
            }
        }

        // Delete category
        public static async Task DeleteCategoryAsync(int idDc)
        {
            using var db = new ShopContext();

            var category = await db.Categories.FirstOrDefaultAsync(x => x.CategoryId == idDc);
            if (category == null)
            {
                Console.WriteLine("Category not found");
                return;
            }

            var categoryWithProduct = await db.Products.AnyAsync(p => p.ProductId == idDc);
            if (categoryWithProduct)
            {
                Console.WriteLine("Category has one or several products attached and cannot be deleted");
                return;
            }
            db.Categories.Remove(category);

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("Category deleted");
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine("DB error: " + exception.GetBaseException().Message);
            }
        }
    }
}
