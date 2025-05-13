using System.Data;
using AgriEnergyConnect.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Models
{
    /// <summary>
    /// Global view of the database.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Automatic Properties
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Farmer> Farmer { get; set; }

        public DbSet<Discussion> Discussion { get; set; }

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pre-populate the AgriEnergyConnect database.
            modelBuilder.Entity<Employee>().HasData(new Employee[]
            {
                new() { EmployeeID = 1, FirstName = "John", LastName = "Lucas", EmailAddress = "john@example.com", CellphoneNumber = "123456789", Gender = "M", UserName = "John", Password = "abcd12!@" },
                new() { EmployeeID = 2, FirstName = "Oliver", LastName = "Lucas", EmailAddress = "oliver@example.com", CellphoneNumber = "123456781", Gender = "M", UserName = "Oliver", Password = "abcd12!@"},
                new() { EmployeeID = 3, FirstName = "Amanda", LastName = "Nicole", EmailAddress = "amanda@example.com", CellphoneNumber = "123456782", Gender = "F", UserName = "Amanda", Password = "abcd12!@" }
            });

            // Pre-populate the AgriEnergyConnect database.
            modelBuilder.Entity<Farmer>().HasData(new Farmer[]
            {
                new() { FarmerID = 1, FirstName = "Marcus", LastName = "John", EmailAddress = "marcus@example.com", CellphoneNumber = "123456783", Gender = "M", UserName = "Marcus", Password = "abcd12!@" },
                new() { FarmerID = 2, FirstName = "Wyatt", LastName = "John", EmailAddress = "wyatt@example.com", CellphoneNumber = "123456784", Gender = "M", UserName = "Wyatt", Password = "abcd12!@" }
            });

            // Pre-populate the AgriEnergyConnect database.
            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new() { ProductID = 1, FarmerID = 1, Name = "Milk", Category = "Dairy", ProductionDate = DateTime.Parse("2025/01/01 00:00:00"), ProductType = "Dairy Product" },
                new() { ProductID = 2, FarmerID = 1, Name = "Chicken", Category = "Meat", ProductionDate = DateTime.Parse("2025/01/07 00:00:00"), ProductType = "Meat Product" },
                new() { ProductID = 3, FarmerID = 1, Name = "Oil", Category = "Oil", ProductionDate = DateTime.Parse("2025/02/06 00:00:00"), ProductType = "Oil Product" },

                new() { ProductID = 4, FarmerID = 2, Name = "Bread", Category = "Wheat", ProductionDate = DateTime.Parse("2025/03/05 00:00:00"), ProductType = "Wheat Product" },
                new() { ProductID = 5, FarmerID = 2, Name = "Eggs", Category = "Meat", ProductionDate = DateTime.Parse("2025/02/18 00:00:00"), ProductType = "Meat Product" },
                new() { ProductID = 6, FarmerID = 2, Name = "Lamb", Category = "Meat", ProductionDate = DateTime.Parse("2025/07/27 00:00:00"), ProductType = "Meat Product" },
            });
        }
    }
}
