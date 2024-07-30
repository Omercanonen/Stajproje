using Microsoft.EntityFrameworkCore;

namespace Stajproje.Models
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options) 
        {   
        }
        public DbSet<Service> Services { get; set; }
    }
}
