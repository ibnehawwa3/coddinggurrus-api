using AutoMapper;
using Coddinggurrus.Api.Models.Admin.User;
using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Models.User;

namespace Coddinggurrus.Api.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserProfileRequest, UserProfileInformation>();
            CreateMap<UserProfiles,UserProfileInformation>();
            CreateMap<UserProfileInformation,UserProfiles>();
        }
    }
}
