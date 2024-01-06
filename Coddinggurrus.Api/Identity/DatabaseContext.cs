using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coddinggurrus.Api.Identity
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
    public class ApplicationUser : IdentityUser
    {
        // Additional properties, if any
        public string FirstName { get; set; }
        public DateTime DateRegistration { get; set; }
    }
}
