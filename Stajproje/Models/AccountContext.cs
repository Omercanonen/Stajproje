using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Stajproje.Models
{
    public class AccountContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Account> Accounts { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options) //dbye ulaşmak isteyen kullanıcı buraya gelir ve dbcontext'e gönderilir.
            :base(options) // base'i dbcontext
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>()
                .HasData(
                    new Account() { id = 1, Username = "admin", Password = "admin" }
                    );
        }
    }
}
