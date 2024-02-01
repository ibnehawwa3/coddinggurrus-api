using Coddinggurrus.Core.Interfaces.Repositories.Course;
using Coddinggurrus.Core.Models.Course;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Infrastructure.Repositories.Course
{
    public class CourseRepository : BaseRepository, ICourseRepository
    {
        public CourseRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<CourseModel>> GetCourses(int skip, int take, string searchText = "")
        {
            var countSql = @$"SELECT COUNT(*) 
                     FROM dbo.Course a with (nolock) 
                     WHERE a.Title like '%{searchText}%'";
            string sql;
            if (string.IsNullOrEmpty(searchText))
            {
                sql = @$"SELECT a.Id, a.Title, a.Description
             FROM dbo.Course a with (nolock)                       
             ORDER BY a.CreatedBy desc                        
             OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY
             {countSql}";
            }
            else
            {
                sql = @$"SELECT a.Id, a.Title, a.Description
             FROM dbo.Course a with (nolock)
             WHERE a.Title like '%{searchText}%'                        
             ORDER BY a.CreatedBy desc
             OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY
             {countSql}";
            }
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var grid = await connection.QueryMultipleAsync(sql, new { searchText, skip, take });
            var articles = grid.Read<CourseWithCount>().ToList();
            var TotalCount = grid.Read<int>().FirstOrDefault();
            articles.ForEach(article => article.TotalCount = TotalCount);

            grid.Dispose();
            return articles;
        }
    }
}
