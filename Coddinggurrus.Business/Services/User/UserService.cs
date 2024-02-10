using Coddinggurrus.Core.Helper;
using AutoMapper;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Core.Models.User;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.User
{
    public class UserService : BaseService , IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IConfiguration config, IUserRepository userRepository, IMapper mapper) : base(config, mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserModel>> GetList(ListingParameter listingParameter)
        {
            return await _userRepository.GetList(listingParameter);
        }
    }
}
