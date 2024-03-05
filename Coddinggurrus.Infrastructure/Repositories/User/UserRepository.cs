using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Models.User;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Coddinggurrus.Infrastructure.Repositories.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

        public async Task<List<UserProfileModel>> GetList(ListingParameter listingParameter)
        {
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var param = new
            {
                @pageNo = listingParameter.Skip,
                @pageSize = listingParameter.Take,
                @searchQuery = listingParameter.TextToSearch
            };
            var list = (await connection.QueryAsync<UserProfileModel>("CoddingGurrus_Dev_GetUserList", param, commandType: CommandType.StoredProcedure)).ToList();
            return list;
        }
    }
}
