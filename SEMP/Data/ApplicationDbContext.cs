using Microsoft.EntityFrameworkCore;
using SEMP.Models;

namespace SEMP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        
        // DbSet properties for your entities
        // For example:
         public DbSet<Event> Events { get; set; }
    }
}
