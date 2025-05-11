using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.Tables
{
    /// <summary>
    /// Product entity for the database.
    /// </summary>
    public class Product
    {
        // Automatic Properties
        [Key]
        public int ProductID { get; set; }
        public int FarmerID { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? ProductionDate { get; set; }
        public string? ProductType { get; set; }
    }
}
