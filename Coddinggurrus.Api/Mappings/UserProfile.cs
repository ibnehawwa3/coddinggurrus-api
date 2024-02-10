using AutoMapper;
using Coddinggurrus.Api.Models.Admin.User;
using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Models.User;
using Coddinggurrus.Infrastructure.APIRequestModels.User;

namespace Coddinggurrus.Api.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserProfileRequest, UserProfiles>();
            CreateMap<UserProfiles,UserProfileInformation>();
            CreateMap<RegisterRequest, UserProfiles>();
        }
    }
}
