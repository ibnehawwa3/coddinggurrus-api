using Coddinggurrus.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Coddinggurrus.Core.Entities.Role;

namespace Coddinggurrus.Infrastructure.Identity
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public DbSet<ApplicationUser> ApplicationUser { set; get; }
        public DbSet<ApplicationRole> ApplicationRole { set; get; }
    }
}
