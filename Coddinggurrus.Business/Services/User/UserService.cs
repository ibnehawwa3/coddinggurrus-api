﻿using Coddinggurrus.Core.Helper;
using AutoMapper;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Core.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Coddinggurrus.Business.Services.User
{
    public class UserService : BaseService , IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IConfiguration config, IUserRepository userRepository, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserProfileModel>> GetList(ListingParameter listingParameter)
        {
            return await _userRepository.GetList(listingParameter);
        }
    }
}
