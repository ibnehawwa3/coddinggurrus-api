using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials.Web
{
    public class WidgetsRepository : BaseRepository, IWidgetsRepository
    {
        public WidgetsRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Course>?> GetBrowseTopics()
        {
            string sql = @"
                           SELECT TOP(14) a.Id, a.Title, a.Image
                           FROM dbo.Course a with (nolock)
                           WHERE a.IsActive = 0
                           ORDER BY a.CreatedBy DESC";

            using (SqlConnection connection = new SqlConnection(CoddingGurrusDbConnectionString))
            {
                var courses = await connection.QueryAsync<Course>(sql);
                return courses;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Course>> GetCoursesForSlider()
        {
            string sql = @"
                           SELECT a.Id, a.Title
                           FROM dbo.Course a with (nolock)
                           WHERE a.IsActive = 0
                           ORDER BY a.CreatedBy DESC";

            using (SqlConnection connection = new SqlConnection(CoddingGurrusDbConnectionString))
            {
                var courses = await connection.QueryAsync<Course>(sql);
                return courses;
            }
        }
    }
}
