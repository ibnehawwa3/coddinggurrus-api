using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials.Web
{
    public class WebCourseRepository : BaseRepository, IWebCourseRepository
    {
        public WebCourseRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }

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
