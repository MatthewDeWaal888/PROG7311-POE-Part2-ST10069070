using System.Text.Json.Serialization;

namespace AgriEnergyConnect.Models.Serializables
{
    [JsonSerializable(typeof(UserProfile))]
    public class UserProfile
    {
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? CellphoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? UserName { get; set; }

    }
}
