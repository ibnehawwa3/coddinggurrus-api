using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Models.User;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Coddinggurrus.Infrastructure.Repositories.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<List<UserModel>> GetList(int pageNo, int pageSize, string searchQuery)
        {
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var param = new
            {
                @pageNo = pageNo,
                @pageSize = pageSize,
                @searchQuery = searchQuery
            };
            var list = (await connection.QueryAsync<UserModel>("CoddingGurrus_Dev_GetUserList", param, commandType: CommandType.StoredProcedure)).ToList();
            return list;
        }
    }
}
