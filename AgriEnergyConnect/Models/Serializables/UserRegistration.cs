using System.Text.Json.Serialization;

namespace AgriEnergyConnect.Models.Serializables
{
    [JsonSerializable(typeof(UserRegistration))]
    public class UserRegistration
    {
        // Automatic Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string CellphoneNumber { get; set; }
        public char Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
