using System.Text.Json.Serialization;

namespace AgriEnergyConnect.Models.Serializables
{
    /// <summary>
    /// This class is used for serialization with JSON and is not an entity
    /// for the database.
    /// </summary>
    [JsonSerializable(typeof(Product))]
    public class Product
    {
        // Automatic Properties
        public string? FarmerID { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? ProductionDate { get; set; }
        public string? ProductType { get; set; }
    }
}
