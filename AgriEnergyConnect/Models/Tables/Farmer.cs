using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.Tables
{
    public class Farmer
    {
        [Key]
        public int FarmerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string CellphoneNumber { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
