using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Menu;
using Coddinggurrus.Api.Models.Admin.RoleMenuPermission;
using Coddinggurrus.Core.Entities;

namespace Coddinggurrus.Api.Mappings
{
    public class RoleMenuPermissionProfile : Profile
    {
        public RoleMenuPermissionProfile()
        {
            CreateMap<RoleMenuPermissionModel, RoleMenuPermission>();
        }
    }
}
 