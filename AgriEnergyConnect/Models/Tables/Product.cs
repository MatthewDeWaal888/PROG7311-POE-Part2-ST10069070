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
        [StringLength(120)]
        public string? Name { get; set; }
        [StringLength(120)]
        public string? Category { get; set; }
        public DateTime? ProductionDate { get; set; }
        [StringLength(120)]
        public string? ProductType { get; set; }
    }
}
