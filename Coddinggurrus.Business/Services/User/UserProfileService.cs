using AutoMapper;
using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Core.Models.User;
using Coddinggurrus.Infrastructure.Enums;
using Coddinggurrus.Infrastructure.Exceptions;
using Coddinggurrus.Infrastructure.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
namespace Coddinggurrus.Business.Services.User
{
    public class UserProfileService : BaseService, IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileService(IUserProfileRepository userProfileRepository, IConfiguration config, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _userProfileRepository = userProfileRepository;
        }

        public bool AddProfile(UserProfiles userProfiles)
        {
            return _userProfileRepository.Add(userProfiles);
        }

        public bool DeleteUser(string id)
        {
            UserProfiles dbUserProfile = _userProfileRepository.GetByUserId(id);
            if (dbUserProfile.IsNull())
                throw new GenericException(ErrorMessages.USER_PROFILE_NOT_EXIST);
            return _userProfileRepository.Delete(id);
        }

        public UserProfileInformation GetUserProfileInformation(string id)
        {
            UserProfiles userProfile = _userProfileRepository.GetByUserId(id);
            if (userProfile.IsNull())
                throw new GenericException(ErrorMessages.USER_PROFILE_NOT_EXIST);
            return Mapper.Map<UserProfileInformation>(userProfile);
        }

        public bool Update(UserProfileInformation userProfileInformation)
        {
            UserProfiles dbUserProfile = _userProfileRepository.GetByUserId(userProfileInformation.UserId);
            if (dbUserProfile.IsNull())
                throw new GenericException(ErrorMessages.USER_PROFILE_NOT_EXIST);
            // Map userProfile to dbUserProfile
            Mapper.Map(userProfileInformation,dbUserProfile);
            // Update additional fields
            dbUserProfile.UpdatedOn = DateTime.Now;
            return _userProfileRepository.Update(dbUserProfile);
        }
    }
}
