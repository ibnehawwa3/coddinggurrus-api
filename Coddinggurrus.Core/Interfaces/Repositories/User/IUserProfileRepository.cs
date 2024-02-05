
using Coddinggurrus.Core.Entities.User;

namespace Coddinggurrus.Core.Interfaces.Repositories.User
{
    public interface IUserProfileRepository
    {
        UserProfiles GetByUserId(string userId);

        bool Update(UserProfiles userProfiles);

        bool Delete(string Id);

        bool Add(UserProfiles userProfile);
    }
}
