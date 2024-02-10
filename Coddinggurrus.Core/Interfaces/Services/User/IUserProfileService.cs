
using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Models.User;

namespace Coddinggurrus.Core.Interfaces.Services.User
{
    public interface IUserProfileService
    {
        bool AddProfile(UserProfiles userProfiles);
        bool DeleteUser(string id);
        UserProfileInformation GetUserProfileInformation(string id);
        bool Update(UserProfileInformation userProfileInformation);
    }
}
