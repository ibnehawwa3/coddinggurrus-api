using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Helper;

namespace Coddinggurrus.Core.Interfaces.Services.Menus
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetMenus(ListingParameter listingParameter);
        Task<Menu> GetMenuById(int id);
        Task<int> AddMenu(Menu menu);
        Task<bool> NameExists(string name);
        Task<bool> UpdateMenu(Menu model);
        Task<bool> DeleteMenu(long Id);
    }
}
