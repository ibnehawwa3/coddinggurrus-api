using Coddinggurrus.Core.Entities;

namespace Coddinggurrus.Core.Interfaces.Services.RoleMenuPermissions
{
    public interface IRoleMenuPermissionService
    {
        Task<IEnumerable<RoleMenuPermission>> GetRoleMenuPermission(string RoleId);
        Task<int> AddRoleMenuPermission(List<RoleMenuPermission> roleMenuPermission);
        Task<bool> UpdateRoleMenuPermission(RoleMenuPermission model);
        Task<bool> DeleteRoleMenuPermission(long Id);
    }
}
