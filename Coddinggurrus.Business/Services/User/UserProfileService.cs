using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.Enums;
using Coddinggurrus.Infrastructure.Exceptions;
using Coddinggurrus.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.User
{
    public class UserProfileService : BaseService, IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileService(IUserProfileRepository userProfileRepository, IConfiguration config) : base(config)
        {
            _userProfileRepository = userProfileRepository;
        }

        public bool DeleteUser(string id)
        {
            UserProfiles dbUserProfile = _userProfileRepository.GetByUserId(id);
            if (dbUserProfile.IsNull())
                throw new GenericException(ErrorMessages.USER_PROFILE_NOT_EXIST);
            return _userProfileRepository.Delete(id);
        }
    }
}
