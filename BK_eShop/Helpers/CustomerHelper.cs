using BK_eShop.Data;
using BK_eShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Helpers
{
    public static class CustomerHelper
    {
        public static async Task CustomerCommandAsync()
        {
            while (true)
            {
                Console.WriteLine("\nCustomer commands: 1. List customers | 2. Add customer | 3. Edit customer | 4. Delete customer | 5. Orders per customers | 6. Exit to main menu");
                Console.Write("> ");

                var customerInput = Console.ReadLine();

                // Skip empty rows
                if (string.IsNullOrEmpty(customerInput))
                {
                    continue;
                }

                // Exit to main menu
                if (customerInput.Equals("6"))
                {
                    break;
                }

                var customerParts = customerInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var customerCommand = customerParts[0].ToLowerInvariant();

                switch (customerCommand)
                {
                    case "1":
                        // List customers
                        await ListCustomersAsync();
                        break;
                    case "2":
                        // Add customer
                        await AddCustomerAsync();
                        break;
                    case "3":
                        // Edit customer
                        if (customerParts.Length < 2 || !int.TryParse(customerParts[1], out var idE))
                        {
                            Console.WriteLine("No customers to edit");
                            break;
                        }
                        await EditCustomerAsync(idE);
                        break;
                    case "4":
                        // Delete customer
                        if (customerParts.Length < 2 || !int.TryParse(customerParts[1], out var idD))
                        {
                            Console.WriteLine("No customers to delete");
                            break;
                        }
                        await DeleteCustomerAsync(idD);
                        break;
                    case "5":
                        // Order count
                        break;
                    default:
                        break;
                }
            }
        }

        // List customers
        public static async Task ListCustomersAsync()
        {
            using var db = new ShopContext();

            var customers = await db.Customers.AsNoTracking().OrderBy(customer => customer.CustomerId).ToListAsync();
            Console.WriteLine("Customer Id | Name | Phone | Email");

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.CustomerId} | {customer.CustomerName} | {customer.CustomerPhone} | {customer.CustomerEmail}");
            }
        }

        // Add customer
        public static async Task AddCustomerAsync()
        {
            Console.Write("Name: ");
            var customerName = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(customerName) || customerName.Length > 150)
            {
                Console.WriteLine("Name is required and cannot be more than 150 characters");
                return;
            }

            // TODO Add phone
            //Console.Write("Phone number: ");
            

            Console.Write("Email: ");
            var customerEmail = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(customerEmail) || customerEmail.Length > 150)
            {
                Console.WriteLine("Email is required and cannot be more than 150 characters");
                return;
            }

            using var db = new ShopContext();
            db.Customers.Add(new Customer { CustomerName = customerName, CustomerEmail = customerEmail });

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("Customer added");
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine("Db error (maybe duplicate)" + exception.GetBaseException().Message);
            }
        }

        // Edit customer
        public static async Task EditCustomerAsync(int idE)
        {
            using var db = new ShopContext();

            var customer = await db.Customers.FirstOrDefaultAsync(x => x.CustomerId == idE);

            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }

            // Change customer name
            Console.WriteLine($"Previous name: {customer.CustomerName}");
            Console.Write("New name: ");
            var customerName = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(customerName))
            {
                customer.CustomerName = customerName;
            }

            // TODO Change customer phone 

            // Change customer email
            Console.WriteLine($"Previous email: {customer.CustomerEmail}");
            Console.Write("New email: ");
            var customerEmail = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(customerEmail))
            {
                customer.CustomerEmail = customerEmail;
            }

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("Edited customer");
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        // Delete customer
        public static async Task DeleteCustomerAsync(int idD)
        {
            using var db = new ShopContext();

            var customer = await db.Customers.FirstOrDefaultAsync(x => x.CustomerId == idD);
            if (customer == null)
            {
                Console.WriteLine("Customer not found");
                return;
            }

            var customerWithOrders = await db.Orders.AnyAsync(o => o.CustomerId == idD);
            if (customerWithOrders)
            {
                Console.WriteLine("Customer has an order so you can't delete them");
                return;
            }

            db.Customers.Remove(customer);

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("Customer deleted");
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine("DB error: " + exception.GetBaseException().Message);
            }
        }
    }
}
