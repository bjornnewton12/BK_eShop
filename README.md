# BK eShop
## Project description
BK eShop is a console-based e-commerce application built with .NET 8, Entity Framework Core, and SQLite.
## This Project Demonstrates
### Entity Framework Core Modeling
The app uses EF Core to define a relational schema consisting of Customers, Orders, OrderRows, Products, and Categories.
Navigation properties and foreign keys illustrate one-to-many relationships.
### ER-diagram
<img width="1217" height="917" alt="BK_eShop_ER" src="https://github.com/user-attachments/assets/3273388a-6dc5-4e4d-9b5d-ab2df6d7a690" />

### Triggers
Order totals are automatically calculated using SQLite triggers that run after inserting, updating, or deleting order rows.
### CRUD
* Every domain object can be managed through the console interface:
  * Customers: create, list, edit, delete
  * Orders: create new orders for customers, list all orders
  * Products: create, list, delete
  * Categories: create, list, delete
### Data Validation
Input validation is handled both through EF Core attributes ([Required], [MaxLength]) and Business rules (stock checks, positive quantity, unique values, category restrictions).
## How to Use the Project
When running the application, the console will guide you with available commands.
### Main Menu
#### Customers  
#### Orders
#### Products  
#### Categories  
### Customers
#### List customers
Displays Customer Id, Customer name, Customer phone and Customer email.
#### List customer name
Displays Customer Id and Customer name.
#### Add customer
Prompts for Customer name, Customer phone and Customer email (UNIQUE).
#### Edit customer
Update Customer name, Customer phone, or Customer email for an existing customer.
#### Delete customer
Deletes a customer, unless they have an order/-s.
### Orders
#### List orders
Shows order ID, date, status, customer name, and auto-calculated total amount.
#### Add order
Select a customer, Add one or more products, Select quantities (validated against stock). Order total is calculated automatically via database triggers.
#### Delete order
Deletes an order
### Products
#### List products
Displays Product name, Category, Product price, and Product stock.
#### Add product
Specify Product name (UNIQUE), Product price, Product stock. Assign a category.
#### Delete product
Deletes a product unless it is referenced in an order.
#### List product by category
Select a category Id and filter all products in that category.
### Categories
#### List categories
Displays all categories.
#### Add category
Creates a new Category name (UNIQUE).
#### Delete category
Allowed only if no products are currently using it.
## Known limitations
* After jumping out of a Switch the user is pushed back to the main menu.
* Order status is never updated. A future update could be to have a choice of Update Order where status can be changed to shipped and then Recieved, when status is Received the order then dissapears after a few days.
* Products are not shown under Orders.
* New products needs to belong to an existing Category. A future update would have an option to add category if needed under Add Product.
* Product quantity never changes after an order has been placed.
## Credits
* Developed by: bjornnewton12
* Technologies Used:
  * .NET 8
  * Entity Framework Core
  * SQLite
  * C# Console Application
