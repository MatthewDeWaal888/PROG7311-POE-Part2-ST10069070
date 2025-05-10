using System.Text.Json.Serialization;

namespace AgriEnergyConnect.Models.Serializables
{
    [JsonSerializable(typeof(Product))]
    public class Product
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? ProductionDate { get; set; }
        public string? ProductType { get; set; }
    }
}
