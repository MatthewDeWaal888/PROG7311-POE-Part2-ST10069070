using System.Text.Json.Serialization;

namespace AgriEnergyConnect.Models.Serializables
{
    [JsonSerializable(typeof(ProductFilter))]
    public class ProductFilter
    {
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductionDate { get; set; }
        public string? ProductType { get; set; }
    }
}
