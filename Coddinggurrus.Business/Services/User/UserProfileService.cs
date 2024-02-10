using AutoMapper;
using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Core.Models.User;
using Coddinggurrus.Infrastructure.Enums;
using Coddinggurrus.Infrastructure.Exceptions;
using Coddinggurrus.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;
using System.Net.Mail;

namespace Coddinggurrus.Business.Services.User
{
    public class UserProfileService : BaseService, IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileService(IUserProfileRepository userProfileRepository, IConfiguration config, IMapper mapper) : base(config, mapper)
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

        public bool Update(UserProfiles userProfile)
        {
            UserProfiles dbUserProfile = _userProfileRepository.GetByUserId(userProfile.UserId);
            if (dbUserProfile.IsNull())
                throw new GenericException(ErrorMessages.USER_PROFILE_NOT_EXIST);

            dbUserProfile.StreetNumber = userProfile.StreetNumber;
            dbUserProfile.Town = userProfile.Town;
            dbUserProfile.ZipCode = userProfile.ZipCode;
            dbUserProfile.EmailAddress = userProfile.EmailAddress;
            dbUserProfile.FirstName = userProfile.FirstName;
            dbUserProfile.LastName = userProfile.LastName;
            dbUserProfile.MobileNumber = userProfile.MobileNumber;
            dbUserProfile.UpdatedOn = DateTime.UtcNow;
            dbUserProfile.Country = userProfile.Country;
            dbUserProfile.CountryCode = userProfile.CountryCode;

            return _userProfileRepository.Update(dbUserProfile);
        }
    }
}
