# BK eShop
## Project description
BK eShop is a console-based e-commerce application built with .NET 8, Entity Framework Core, and SQLite.
Users can:
* View, add, edit and delete customers
* View, add and delete orders
* View, add and delete products as well as filter products based on category
* View, add and delete categories
## This Project Demonstrates
### Entity Framework Core Modeling
The app uses EF Core to define a relational schema consisting of Customers, Orders, OrderRows, Products, and Categories.
Navigation properties and foreign keys illustrate one-to-many relationships.
### Triggers
Order totals are automatically calculated using SQLite triggers that run after inserting, updating, or deleting order rows.
### CRUD
* Every domain object can be managed through the console interface:
* Customers: create, list, edit, delete
* Orders: create new orders for customers, list all orders
* Products: create, list, delete
* Categories: create, list, delete
### Data Validation
Input validation is handled both through:
* EF Core attributes ([Required], [MaxLength])
* Business rules (stock checks, positive quantity, unique values, category restrictions)
### The app uses unique indexes on:
* Customer email
* Product name
* Category name
## How to Use the Project
When running the application, the console will guide you with available commands.
### Main Menu
* Customers  
* Orders
* Products  
* Categories  
### Customers
* List customers
  * Displays Customer Id, Customer name, Customer phone (Encrypted), Customer email (Encrypted)
* Add customer
  * Prompts for Customer name, Customer phone and Customer email (UNIQUE)
* Edit customer
  * Update Customer name, Customer phone, or Customer email for an existing customer
* Delete customer
  * Removes a customer, unless they have an order/-s
### Orders
* List orders
  * Shows order ID, date, status, customer name, and auto-calculated total amount
* Add order
  * Select a customer
  * Add one or more products
  * Select quantities (validated against stock)
  * Order total is calculated automatically via database triggers
### Products
* List products
  * Displays Product name, Category, Product price, and Product stock
* Add product
  * Specify Product name (UNIQUE), Product price, Product stock
  * Assign a category
* Delete product
  * Removes a product unless it is referenced in an order
* List product by category
  * Select a category Id and display all products in that category
### Categories
* List categories
  * Displays all categories
* Add category
  * Creates a new Category name (UNIQUE)
* Delete category
  * Allowed only if no products are currently using it
## Known limitations
* After jumping out of a Switch the user is pushed back to the main menu
* Phone and email are encrypted under List Customer but not under Edit Customer
* Each customer has a password but the password is never used
  * A future update of the project would be to select a Customer Id and write their password to edit customer and view their orders, as well as have an admin password login and password to edit everything.
* Order status is never updated
  * A future update could be to have a choice of Update Order where status can be changed to shipped and then Recieved, when Received is chosen the order then dissapears after a few days.
* Products are not shown under Orders
* Product quantity never changes after an order has been placed
## Credits
* Developed by: bjornnewton12
* Technologies Used:
  * .NET 8
  * Entity Framework Core
  * SQLite
  * C# Console Application
