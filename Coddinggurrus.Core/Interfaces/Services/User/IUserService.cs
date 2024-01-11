
using Coddinggurrus.Core.Models.User;

namespace Coddinggurrus.Core.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<List<UserModel>> GetList(int pageNo, int pageSize, string searchText);
    }
}
