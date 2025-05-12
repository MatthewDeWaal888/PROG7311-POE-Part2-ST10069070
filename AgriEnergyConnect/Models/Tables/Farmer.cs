using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.Tables
{
    /// <summary>
    /// Farmer entity for the database.
    /// </summary>
    public class Farmer
    {
        // Automatic Properties
        [Key]
        public int FarmerID { get; set; }
        [StringLength(120)]
        public string? FirstName { get; set; }
        [StringLength(120)]
        public string? LastName { get; set; }
        [StringLength(255)]
        public string? EmailAddress { get; set; }
        [StringLength(120)]
        public string? CellphoneNumber { get; set; }
        [StringLength(1)]
        public string? Gender { get; set; }
        [StringLength(120)]
        public string? UserName { get; set; }
        [StringLength(255)]
        public string? Password { get; set; }
    }
}
