using Coddinggurrus.Core.Entities;

namespace Coddinggurrus.Core.Interfaces.Repositories.RoleMenuPermissions
{
    public interface IRoleMenuPermissionRepositry
    {
        Task<IEnumerable<RoleMenuPermission>> GetRoleMenuPermission(string RoleId);
        Task<int> AddRoleMenuPermission(List<RoleMenuPermission> roleMenuPermission);
        Task<bool> UpdateRoleMenuPermission(RoleMenuPermission model);
        Task<bool> DeleteRoleMenuPermission(long Id);
    }
}
