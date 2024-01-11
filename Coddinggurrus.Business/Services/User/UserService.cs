﻿using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Core.Models.User;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.User
{
    public class UserService : BaseService , IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IConfiguration config, IUserRepository userRepository) : base(config)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserModel>> GetList(int pageNo, int pageSize, string searchText)
        {
            return await _userRepository.GetList(pageNo,pageSize,searchText);
        }
    }
}
