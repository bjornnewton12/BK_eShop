using BK_eShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BK_eShop.Helpers
{
    public static class OrderHelper
    {
        public static async Task ListOrdersAsync()
        {
            using var db = new ShopContext();

            var sw = System.Diagnostics.Stopwatch.StartNew();

            var allOrders = await db.Orders.AsNoTracking()
                .Include(x => x.Customer)
                .OrderBy(order => order.OrderId).ToListAsync();

            sw.Stop();
            Console.WriteLine($"Total query time: {sw.ElapsedMilliseconds} ms");

            Console.WriteLine("\nOrder Id | Order date | Status | Customer name | Total amount");

            foreach (var allOrder in allOrders)
            {
                Console.WriteLine($"{allOrder.OrderId} | {allOrder.OrderDate} | {allOrder.OrderStatus} | {allOrder.Customer?.CustomerName} |  {allOrder.OrderTotalAmount}");
            }
        }

        public static async Task AddOrderAsync(int customerId)
        {
            using var db = new ShopContext();

            if (!await db.Customers.AnyAsync(c => c.CustomerId == customerId))
            {
                Console.WriteLine("Customer not found");
                return;
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                OrderTotalAmount = 0
            };

            var orderRows = new List<OrderRow>();
            var choice = "";
            while (choice != "2")
            {
                await ProductHelper.ListProductsAsync();

                Console.WriteLine("\nSelect a product Id: ");
                var prodId = Console.ReadLine();
                if (!int.TryParse(prodId, out var idP))
                {
                    Console.WriteLine("Invalid product Id");
                    continue;
                }

                var product = await db.Products.FirstOrDefaultAsync(p => p.ProductId == idP);
                if (product == null)
                {
                    Console.WriteLine("Product not found");
                    continue;
                }

                Console.Write("\nSelect quantity: ");
                var oQuantity = Console.ReadLine();
                if (!int.TryParse(oQuantity, out var orderQuantity))
                {
                    Console.WriteLine("You need to input numbers");
                    continue;
                }

                if (orderQuantity <= 0)
                {
                    Console.WriteLine("Quantity must be a positive number");
                    continue;
                }

                if (product.ProductStock < orderQuantity)
                {
                    Console.WriteLine("Order quantity cannot exceed product quantity");
                    continue;
                }

                var oRow = new OrderRow
                {
                    ProductId = product.ProductId,
                    OrderRowQuantity = orderQuantity,
                    OrderRowUnitPrice = product.ProductPrice,
                    Order = order
                };

            orderRows.Add(oRow);

            Console.WriteLine("\nWould you like to add another product to your order? (1. Yes | 2. No)");
            choice = Console.ReadLine() ?? "2";
            }
            order.OrderRows = orderRows;
            db.Orders.Add(order);

            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine($"Order complete - Order ID: {order.OrderId}");
            }

            catch (DbUpdateException ex)
            {
                Console.WriteLine("Db Error: " + ex.GetBaseException().Message);
            }
        }

        // Delete order
        public static async Task DeleteOrderAsync(int idO)
        {
            using var db = new ShopContext();

            var order = await db.Orders.FirstOrDefaultAsync(x => x.OrderId == idO);
            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }

            db.Orders.Remove(order);

            // Save changes
            try
            {
                await db.SaveChangesAsync();
                Console.WriteLine("Order deleted");
            }
            catch (DbUpdateException exception)
            {
                Console.WriteLine("DB error: " + exception.GetBaseException().Message);
            }
        }
    }
}