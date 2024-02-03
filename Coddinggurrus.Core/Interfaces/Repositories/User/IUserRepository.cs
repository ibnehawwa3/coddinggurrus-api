
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Models.User;

namespace Coddinggurrus.Core.Interfaces.Repositories.User
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetList(ListingParameter listingParameter);
    }
}
