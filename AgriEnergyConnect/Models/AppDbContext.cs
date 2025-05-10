using System.Data;
using AgriEnergyConnect.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Farmer> Farmer { get; set; }

        public DbSet<Discussion> Discussion { get; set; }
    }
}
