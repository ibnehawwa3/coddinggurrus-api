
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Models.User;

namespace Coddinggurrus.Core.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<List<UserProfileModel>> GetList(ListingParameter listingParameter);
    }
}
