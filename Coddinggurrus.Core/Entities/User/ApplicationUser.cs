using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Core.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties, if any
        public string FirstName { get; set; }
        public DateTime DateRegistration { get; set; }
    }
}
