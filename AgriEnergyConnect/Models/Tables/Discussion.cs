using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.Tables
{
    /// <summary>
    /// Discussion entity for the database.
    /// </summary>
    public class Discussion
    {
        // Automatic Properties
        [Key]
        public int DiscussionId { get; set; }
        public string? Content { get; set; }
        public string? UserName { get; set; }
    }
}
