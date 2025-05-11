using System.Text.Json.Serialization;

namespace AgriEnergyConnect.Models.Serializables
{
    /// <summary>
    /// This class is used for serialization with JSON and is not
    /// and entity for the database.
    /// </summary>
    [JsonSerializable(typeof(ProductFilter))]
    public class ProductFilter
    {
        // Automatic Properties
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductionDate { get; set; }
        public string? ProductType { get; set; }
    }
}
