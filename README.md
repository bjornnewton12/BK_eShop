BK eShop
Project description
BK eShop is a console-based e-commerce application built with .NET 8, Entity Framework Core, and SQLite.
It simulates a simple shop environment where users can:

* Create and manage customers
* Create orders with multiple products
* Automatically calculate order totals
* Add, view, and remove products
* Add and remove categories

The entire system demonstrates fundamental database concepts including relationships, validation, indexing, triggers, and aggregated views.

This Project Demonstrates
Entity Framework Core Modeling

The app uses EF Core to define a relational schema consisting of Customers, Orders, OrderRows, Products, and Categories.
Navigation properties and foreign keys illustrate one-to-many relationships.

SQL Triggers for Business Logic

Order totals are automatically calculated using SQLite triggers that run after inserting, updating, or deleting order rows.
This ensures accurate totals without writing manual update logic in C#.

Database Views

A SQLite view (OrderSummary) is used to present aggregated order information, demonstrating how EF Core can map to read-only database views.

CRUD Operations

Every domain object can be managed through the console interface:

Customers: create, list, edit, delete

Orders: create new orders for customers, list all orders

Products: create, list, delete

Categories: create, list, delete

These flows represent typical CRUD patterns found in real-world applications.

Data Validation

Input validation is handled both through:

EF Core attributes ([Required], [MaxLength])

Business rules (stock checks, positive quantity, unique values, category restrictions)

This ensures data integrity throughout the application.

Database Indexing

The app uses unique indexes on:

Customer email

Product name

Category name

This demonstrates how indexing improves lookup performance and enforces uniqueness.

How to Use the Project

When running the application, the console will guide you with available commands.

Main Menu
1. Customers  
2. Orders  
3. Products  
4. Categories  

🧑 Customers

(1) List customers
Displays all registered customers.

(2) Add customer
Prompts for name, phone, and email (email must be unique).

(3) Edit customer
Update name, phone, or email for an existing customer.

(4) Delete customer
Removes a customer and all related orders.

📦 Orders

(1) List orders
Shows order ID, date, status, customer name, and auto-calculated total amount.

(2) Add order

Select a customer

Add one or more products

Select quantities (validated against stock)

Order total is calculated automatically via database triggers

🛍️ Products

(1) List products
Displays product name, category, price, and stock.

(2) Add product

Specify a name, price, stock

Assign a category

Validates length, numeric input, and category existence

(3) Delete product
Removes a product unless it is referenced in an order.

🗂 Categories

(1) List categories
Shows all categories.

(2) Add category
Creates a new category with a unique name.

(3) Delete category
Allowed only if no products are currently using it.

Credits

Developed by: bjornnewton12
Technologies Used:

.NET 8

Entity Framework Core

SQLite

C# Console Application
