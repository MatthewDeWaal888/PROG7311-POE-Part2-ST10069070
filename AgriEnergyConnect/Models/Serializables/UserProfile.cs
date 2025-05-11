using System.Text.Json.Serialization;

namespace AgriEnergyConnect.Models.Serializables
{
    /// <summary>
    /// This class is used for serialization with JSON and is not
    /// an entity for the database.
    /// </summary>
    [JsonSerializable(typeof(UserProfile))]
    public class UserProfile
    {
        // Automatic Properties
        public int UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? CellphoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? UserName { get; set; }

    }
}
