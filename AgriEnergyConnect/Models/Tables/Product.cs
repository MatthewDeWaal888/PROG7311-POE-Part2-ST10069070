using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.Tables
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public int FarmerID { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? ProductionDate { get; set; }
        public string? ProductType { get; set; }
    }
}
