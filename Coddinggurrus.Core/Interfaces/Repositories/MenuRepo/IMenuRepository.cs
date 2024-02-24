using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Helper;

namespace Coddinggurrus.Core.Interfaces.Repositories.MenuRepo
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetMenus(ListingParameter listingParameter);
        Task<Menu> GetMenuById(int id);
        Task<int> AddMenu(Menu menu);
        Task<bool> NameExists(string title);
        Task<bool> UpdateMenu(Menu model);
        Task<bool> DeleteMenu(long Id);
    }
}
