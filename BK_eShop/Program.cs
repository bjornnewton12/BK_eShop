using BK_eShop;
using BK_eShop.Data;
using BK_eShop.Helpers;
using BK_eShop.Models;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("DB: " + Path.Combine(AppContext.BaseDirectory, "shop.db"));
await Seeding.SeedAsync();

// CLI
while (true)
{
    Console.WriteLine($"\nCommands: 1. Customers | 2. Orders | 3. Products");
    Console.Write(">");
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
            await CustomerHelper.CustomerCommandAsync();
            break;

        // Order commands
        case "2":
            Console.WriteLine("\nOrder commands: 1. List orders | 2. Product list | 3. Add order | 4. Delete order | 5. Exit to main menu");
            Console.Write("> ");

            var orderInput = Console.ReadLine();

            // Skip empty rows
            if (string.IsNullOrEmpty(orderInput))
            {
                continue;
            }

            // Exit to main menu
            if (orderInput.Equals("5"))
            {
                break;
            }

            var orderParts = orderInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var orderCommand = orderParts[0].ToLowerInvariant();

            switch (orderCommand)
            {
                case "1":
                    // List orders
                    break;
                    // Product list
                case "2":
                    break;
                case "3":
                    // Add order
                    break;
                case "4":
                    // Delete order
                    break;
            }
            break;

        // Product commands
        case "3":
            Console.WriteLine("\nCustomer commands: 1. List products | 2. Add Product | 3. Exit to main menu");
            Console.Write("> ");

            var productInput = Console.ReadLine();

            // Skip empty rows
            if (string.IsNullOrEmpty(productInput))
            {
                continue;
            }

            // Exit to main menu
            if (productInput.Equals("3"))
            {
                break;
            }

            var productParts = productInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var productCommand = productParts[0].ToLowerInvariant();

            switch(productCommand)
            {
            case "1":
                // List products
                break;
            case "2":
                // Add product
                break;
            }

            break;
    }
}