using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgriEnergyConnect.Migrations
{
    /// <inheritdoc />
    public partial class AEC_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Farmer",
                keyColumn: "FarmerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Farmer",
                keyColumn: "FarmerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 6);
        }
    }
}
