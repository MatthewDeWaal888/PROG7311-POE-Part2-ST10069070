using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriEnergyConnect.Migrations
{
    /// <inheritdoc />
    public partial class AgriEnergyConnect_ST10069070 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discussion",
                columns: table => new
                {
                    DiscussionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussion", x => x.DiscussionID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CellphoneNumber = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Gender = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Farmer",
                columns: table => new
                {
                    FarmerID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CellphoneNumber = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Gender = table.Column<string>(type: "TEXT", maxLength: 1, nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmer", x => x.FarmerID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FarmerID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProductType = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeID", "CellphoneNumber", "EmailAddress", "FirstName", "Gender", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "123456789", "john@example.com", "John", "M", "Lucas", "abcd12!@", "John" },
                    { 2, "123456781", "oliver@example.com", "Oliver", "M", "Lucas", "abcd12!@", "Oliver" },
                    { 3, "123456782", "amanda@example.com", "Amanda", "F", "Nicole", "abcd12!@", "Amanda" }
                });

            migrationBuilder.InsertData(
                table: "Farmer",
                columns: new[] { "FarmerID", "CellphoneNumber", "EmailAddress", "FirstName", "Gender", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "123456783", "marcus@example.com", "Marcus", "M", "John", "abcd12!@", "Marcus" },
                    { 2, "123456784", "wyatt@example.com", "Wyatt", "M", "John", "abcd12!@", "Wyatt" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductID", "Category", "FarmerID", "Name", "ProductType", "ProductionDate" },
                values: new object[,]
                {
                    { 1, "Dairy", 1, "Milk", "Dairy Product", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Meat", 1, "Chicken", "Meat Product", new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Oil", 1, "Oil", "Oil Product", new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Wheat", 2, "Bread", "Wheat Product", new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Meat", 2, "Eggs", "Meat Product", new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Meat", 2, "Lamb", "Meat Product", new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discussion");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Farmer");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
