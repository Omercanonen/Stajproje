using Microsoft.EntityFrameworkCore;

namespace Stajproje.Models
{
    public class BrandDbContext:DbContext
    {
        public BrandDbContext(DbContextOptions<BrandDbContext> options): base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
