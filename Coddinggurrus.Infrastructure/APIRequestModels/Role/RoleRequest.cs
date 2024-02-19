using Coddinggurrus.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Infrastructure.APIRequestModels.Role
{
    public class RoleRequest
    {
        [Required]
        public string RoleName { get; set; }
        public ApplicationUser User { get; set; }
    }
}
