using AutoMapper;
using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Interfaces.Repositories.RoleMenuPermissions;
using Coddinggurrus.Core.Interfaces.Services.RoleMenuPermissions;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.RoleMenuPermissions
{
    public class RoleMenuPermissionService : BaseService, IRoleMenuPermissionService
    {
        private readonly IRoleMenuPermissionRepositry _roleMenuPermissionRepositry;
        public RoleMenuPermissionService(IRoleMenuPermissionRepositry roleMenuPermissionRepositry, IConfiguration config, IMapper mapper) : base(config, mapper)
        {
            _roleMenuPermissionRepositry = roleMenuPermissionRepositry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleMenuPermission>> GetRoleMenuPermission(string RoleId)
        {
            ///listingParameter.Skip = (listingParameter.Skip * listingParameter.Take) - pageSize;
            return await _roleMenuPermissionRepositry.GetRoleMenuPermission(RoleId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<int> AddRoleMenuPermission(List<RoleMenuPermission> roleMenuPermission)
        {
            return await _roleMenuPermissionRepositry.AddRoleMenuPermission(roleMenuPermission);
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRoleMenuPermission(RoleMenuPermission model)
        {
            return await _roleMenuPermissionRepositry.UpdateRoleMenuPermission(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleMenuPermission(long Id)
        {
            return await _roleMenuPermissionRepositry.DeleteRoleMenuPermission(Id);
        }
    }
}
