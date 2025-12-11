using BK_eShop;
using BK_eShop.Data;
using BK_eShop.Helpers;
using BK_eShop.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;

Console.WriteLine("DB: " + Path.Combine(AppContext.BaseDirectory, "shop.db"));
await Seeding.SeedAsync();

while (true)
{
    Console.WriteLine($"\nCommands: 1. Customers | 2. Orders | 3. Products | 4. Categories");
    Console.Write("> ");
    var firstCommand = Console.ReadLine();

    // Skip empty rows
    if (string.IsNullOrEmpty(firstCommand))
    { 
    continue;
    }

    switch (firstCommand)
    {
        // Customer commands
        case "1":
                Console.WriteLine("\nCustomer commands: 1. List customers | 2. Add customer | 3. Edit customer | 4. Delete customer | 5. Exit to main menu");
                Console.Write("> ");

                var customerInput = Console.ReadLine();

                // Skip empty rows
                if (string.IsNullOrEmpty(customerInput))
                {
                    continue;
                }

                // Exit to main menu
                if (customerInput.Equals("5"))
                {
                    break;
                }

                var customerParts = customerInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var customerCommand = customerParts[0].ToLowerInvariant();

                switch (customerCommand)
                {
                    case "1":
                        // List customers
                        await CustomerHelper.ListCustomersAsync();
                        break;
                    case "2":
                        // Add customer
                        await CustomerHelper.AddCustomerAsync();
                        break;
                    case "3":
                    // Edit customer
                        await CustomerHelper.ListCustomersAsync();
                        Console.Write("\nEnter customer Id to edit: ");
                        var editCustomerInput = Console.ReadLine();
                        
                        if (!int.TryParse(editCustomerInput, out var idE))
                        {
                            Console.WriteLine("There is no customer with that Id");
                            break;
                        }
                        await CustomerHelper.EditCustomerAsync(idE);
                        break;
                    case "4":
                    // Delete customer
                        await CustomerHelper.ListCustomersAsync();
                        Console.Write("\nEnter customer Id to delete: ");
                        var deleteCustomerInput = Console.ReadLine();

                        if (!int.TryParse(deleteCustomerInput, out var idD))
                        {
                            Console.WriteLine("There is no customer with that Id");
                            break;
                        }
                        await CustomerHelper.DeleteCustomerAsync(idD);
                        break;
                    default:
                        break;
                }
                break;

        // Order commands
        case "2":
            Console.WriteLine("\nOrder commands: 1. List orders | 2. Add order | 3. Delete order | 4. Exit to main menu");
            Console.Write("> ");

            var orderInput = Console.ReadLine();

            // Skip empty rows
            if (string.IsNullOrEmpty(orderInput))
            {
                continue;
            }

            // Exit to main menu
            if (orderInput.Equals("4"))
            {
                break;
            }

            var orderParts = orderInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var orderCommand = orderParts[0].ToLowerInvariant();

            switch (orderCommand)
            {
                case "1":
                    // List orders
                    await OrderHelper.ListOrdersAsync();
                    break;
                    
                case "2":
                    // Add order
                    await CustomerHelper.ListCustomerNamesAsync();
                    Console.Write("\nSelect customer Id to add order: ");
                    var customerId = Console.ReadLine();
                    if(!int.TryParse(customerId, out var IdO))
                    {
                        Console.WriteLine("Invalid input, must be numbers");
                        continue;
                    }
                    await OrderHelper.AddOrderAsync(IdO);
                    break;
                case "3":
                    // Delete order
                    await OrderHelper.ListOrdersAsync();
                    Console.Write("\nSelect order Id to delete: ");
                    var deleteOrderInput = Console.ReadLine();

                    if (!int.TryParse(deleteOrderInput, out var idO))
                    {
                        Console.WriteLine("There is no order with that Id");
                        break;
                    }
                    await OrderHelper.DeleteOrderAsync(idO);
                    break;
            }
            break;

        // Product commands
        case "3":
            Console.WriteLine("\nProduct commands: 1. List products | 2. Add Product | 3. Delete product | 4. List product by category | 5. Exit to main menu");
            Console.Write("> ");

            var productInput = Console.ReadLine();

            // Skip empty rows
            if (string.IsNullOrEmpty(productInput))
            {
                continue;
            }

            // Exit to main menu
            if (productInput.Equals("5"))
            {
                break;
            }

            var productParts = productInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var productCommand = productParts[0].ToLowerInvariant();

            switch(productCommand)
            {
            case "1":
                // List products
                await ProductHelper.ListProductsAsync();
                break;
            case "2":
                // Add product
                await ProductHelper.AddProductAsync();
                break;
            case "3":
                //Delete product
                await ProductHelper.ListProductsAsync();
                Console.Write("\nEnter product Id to delete: ");
                var deleteProductInput = Console.ReadLine();

                if (!int.TryParse(deleteProductInput, out var idDp))
                {
                   Console.WriteLine("There is no product with that Id");
                   break;
                }
                  await ProductHelper.DeleteProductAsync(idDp);
                break;
            case "4":
                // List products by category  
                await ProductHelper.ListProductsbyCategoryAsync();
                break;
            }
            break;

        // Category commands
        case "4":
            Console.WriteLine("\nCategory commands: 1. List categories | 2. Add Category | 3.Delete category | 4. Exit to main menu");
            Console.Write("> ");

            var categoryInput = Console.ReadLine();

            // Skip empty rows
            if (string.IsNullOrEmpty(categoryInput))
            {
                continue;
            }

            // Exit to main menu
            if (categoryInput.Equals("4"))
            {
                break;
            }

            var categoryParts = categoryInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var categoryCommand = categoryParts[0].ToLowerInvariant();

            switch (categoryCommand)
            {
                case "1":
                    // List category
                    await CategoryHelper.ListCategoriesAsync();
                    break;
                case "2":
                    // Add category
                    await CategoryHelper.AddCategoryAsync();
                    break;
                case "3":
                    // Delete category
                    await CategoryHelper.ListCategoriesAsync();
                    Console.Write("\nEnter category Id to delete: ");
                    var deleteCategoryInput = Console.ReadLine();

                    if (!int.TryParse(deleteCategoryInput, out var idDc))
                    {
                        Console.WriteLine("There is no category with that name");
                        break;
                    }
                    await CategoryHelper.DeleteCategoryAsync(idDc);
                    break;
            }
            break;
    }
}