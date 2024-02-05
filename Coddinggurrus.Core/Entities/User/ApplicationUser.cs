using Microsoft.AspNetCore.Identity;

namespace Coddinggurrus.Core.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties, if any
        public string FirstName { get; set; }
        public DateTime DateRegistration { get; set; }
    }
}
