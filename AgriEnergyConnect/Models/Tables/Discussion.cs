using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models.Tables
{
    public class Discussion
    {
        [Key]
        public int DiscussionId { get; set; }
        public string? Content { get; set; }
        public string? UserName { get; set; }
    }
}
