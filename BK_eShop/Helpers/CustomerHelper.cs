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
        // List customers
        public static async Task ListCustomersAsync()
        {
            using var db = new ShopContext();

            var customers = await db.Customers.AsNoTracking().OrderBy(customer => customer.CustomerId).ToListAsync();
            Console.WriteLine("\nCustomer Id | Name | Phone (Encrypted) | Email (Encrypted)");

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.CustomerId} | {customer.CustomerName} | {customer.CustomerPhone} | {customer.CustomerEmail}");
            }
        }

        // List customer names
        public static async Task ListCustomerNamesAsync()
        {
            using var db = new ShopContext();

            var customers = await db.Customers.AsNoTracking().OrderBy(customer => customer.CustomerId).ToListAsync();
            Console.WriteLine("\nCustomer Id | Name");

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.CustomerId} | {customer.CustomerName}");
            }
        }

        // Add customer
        public static async Task AddCustomerAsync()
        {
            Console.Write("\nName: ");
            var customerName = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(customerName) || customerName.Length > 150)
            {
                Console.WriteLine("Name is required and cannot be more than 150 characters");
                return;
            }

            Console.Write("Phone number: ");
            var customerPhone = Console.ReadLine()?.Trim() ?? string.Empty;


            Console.Write("Email: ");
            var customerEmail = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(customerEmail) || customerEmail.Length > 150)
            {
                Console.WriteLine("Email is required and cannot be more than 150 characters");
                return;
            }

            Console.Write("Password: ");
            var customerPassword = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(customerPassword))
            {
                Console.WriteLine("Password is required");
                return;
            }

            using var db = new ShopContext();
            db.Customers.Add(new Customer 
            { 
                CustomerName = customerName,
                CustomerPhone = EncryptionHelper.Encrypt(customerPhone),
                CustomerEmail = EncryptionHelper.Encrypt(customerEmail),
                CustomerPassword = EncryptionHelper.Encrypt(customerPassword)
            });

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("\nCustomer added");
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
                Console.WriteLine("\nCustomer not found");
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

            // Change customer phone 
            Console.WriteLine($"Previous phone number: {customer.CustomerPhone}");
            Console.Write("New phone number: ");
            var customerPhone = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(customerPhone))
            {
                customer.CustomerPhone = customerPhone;
            }

            // Change customer email
            Console.WriteLine($"Previous email: {EncryptionHelper.Decrypt(customer.CustomerEmail)}");
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
                Console.WriteLine("\nEdited customer");
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
                Console.WriteLine("\nCustomer not found");
                return;
            }

            var customerWithOrders = await db.Orders.AnyAsync(o => o.CustomerId == idD);
            if (customerWithOrders)
            {
                Console.WriteLine("\nCustomer has an order so you can't delete them");
                return;
            }

            db.Customers.Remove(customer);

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("\nCustomer deleted");
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine("DB error: " + exception.GetBaseException().Message);
            }
        }
    }
}