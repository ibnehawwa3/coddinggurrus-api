using AutoMapper;
using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.MenuRepo;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.Interfaces.Services.Menus;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Infrastructure.Repositories.Tutorials;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Business.Services.Menus
{
    public class MenuService : BaseService, IMenuService
    {

        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository, IConfiguration config, IMapper mapper) : base(config, mapper)
        {
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Menu>> GetMenus(ListingParameter listingParameter)
        {
            ///listingParameter.Skip = (listingParameter.Skip * listingParameter.Take) - pageSize;
            return await _menuRepository.GetMenus(listingParameter);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<int> AddMenu(Menu menu)
        {
            return await _menuRepository.AddMenu(menu);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<bool> NameExists(string name)
        {
            var exists = await _menuRepository.NameExists(name);
            return exists;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMenu(Menu model)
        {
            return await _menuRepository.UpdateMenu(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMenu(long Id)
        {
            return await _menuRepository.DeleteMenu(Id);
        }
    }
}
