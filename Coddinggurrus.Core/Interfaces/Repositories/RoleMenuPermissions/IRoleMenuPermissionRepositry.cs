using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Core.Interfaces.Repositories.RoleMenuPermissions
{
    public interface IRoleMenuPermissionRepositry
    {
        Task<IEnumerable<RoleMenuPermission>> GetRoleMenuPermission(ListingParameter listingParameter);
        Task<int> AddRoleMenuPermission(RoleMenuPermission roleMenuPermission);
        Task<bool> UpdateRoleMenuPermission(RoleMenuPermission model);
        Task<bool> DeleteRoleMenuPermission(long Id);
    }
}
