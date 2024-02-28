using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Menu;
using Coddinggurrus.Core.Entities;

namespace Coddinggurrus.Api.Mappings
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<MenuModel, Menu>();
        }
    }
}
