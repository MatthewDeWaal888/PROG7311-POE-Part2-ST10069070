using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.Tables
{
    /// <summary>
    /// Employee entity for the database.
    /// </summary>
    public class Employee
    {
        // Automatic Properties
        [Key]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string CellphoneNumber { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
