using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BK_eShop.Migrations
{
    /// <inheritdoc />
    public partial class NinthtenthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CustomerPhone = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "OrderSummaries",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    ProductStock = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrderStatus = table.Column<string>(type: "TEXT", nullable: false),
                    OrderTotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRows",
                columns: table => new
                {
                    OrderRowId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderRowQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderRowUnitPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRows", x => x.OrderRowId);
                    table.ForeignKey(
                        name: "FK_OrderRows_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRows_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderRows_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerEmail",
                table: "Customers",
                column: "CustomerEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_OrderId",
                table: "OrderRows",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_ProductId",
                table: "OrderRows",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_ProductId1",
                table: "OrderRows",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true);

            // Order Summary
            migrationBuilder.Sql(@"
                CREATE VIEW IF NOT EXISTS OrderSummary AS
                SELECT
                    o.OrderId,
                    o.OrderDate,
                    c.CustomerName AS CustomerName,
                    c.CustomerEmail AS CustomerEmail,
                    IFNULL(SUM(orw.OrderRowQuantity * orw.OrderRowUnitPrice), 0) AS OrderTotalAmount
                FROM Orders o
                JOIN Customers c ON c.CustomerId = o.CustomerId
                LEFT JOIN OrderRows orw ON orw.OrderId = o.OrderId
                GROUP BY o.OrderId, o.OrderDate, c.CustomerName, c.CustomerEmail;
                ");

            // AFTER INSERT
            migrationBuilder.Sql(@"
                CREATE TRIGGER IF NOT EXISTS trg_OrderRow_Insert
                AFTER INSERT ON OrderRows
                BEGIN
                    UPDATE Orders
                    SET OrderTotalAmount = (
                                       SELECT IFNULL(SUM(OrderRowQuantity * OrderRowUnitPrice), 0) 
                                       FROM OrderRows 
                                       WHERE OrderId = NEW.OrderId
                                      )
                    WHERE OrderId = NEW.OrderId;
                END;                
                ");

            // AFTER UPDATE
            migrationBuilder.Sql(@"
                CREATE TRIGGER IF NOT EXISTS trg_OrderRow_Update
                AFTER UPDATE ON OrderRows
                BEGIN
                    UPDATE Orders
                    SET OrderTotalAmount = ( 
                                        SELECT IFNULL (SUM(OrderRowQuantity * OrderRowUnitPrice), 0)
                                        FROM OrderRows 
                                        WHERE OrderId = NEW.OrderId
                                      )
                    WHERE OrderId = NEW.OrderId;                                  
                END;
                ");

            // AFTER DELETE (Stämmer det att det måste vara OLD?)
            migrationBuilder.Sql(@"
                CREATE TRIGGER IF NOT EXISTS trg_OrderRow_Delete
                AFTER DELETE ON OrderRows
                BEGIN
                    UPDATE Orders
                    SET OrderTotalAmount = ( 
                                        SELECT IFNULL (SUM(OrderRowQuantity * OrderRowUnitPrice), 0)
                                        FROM OrderRows 
                                        WHERE OrderId = OLD.OrderId
                                      )
                    WHERE OrderId = OLD.OrderId;
                END;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRows");

            migrationBuilder.DropTable(
                name: "OrderSummaries");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.Sql(@"
                 DROP VIEW IF EXISTS OrderSummary
                 ");

            migrationBuilder.Sql(@"
                 DROP TRIGGER IF EXISTS trg_OrderRow_Insert
                 ");

            migrationBuilder.Sql(@"
                 DROP TRIGGER IF EXISTS trg_OrderRow_Update
                 ");

            migrationBuilder.Sql(@"
                 DROP TRIGGER IF EXISTS trg_OrderRow_Delete
                 ");
        }
    }
}
