
using Coddinggurrus.Core.Models.User;

namespace Coddinggurrus.Core.Interfaces.Repositories.User
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetList(int pageNo, int pageSize, string searchText);
    }
}
