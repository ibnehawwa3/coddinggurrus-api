using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials
{
    public class CourseRepository : BaseRepository, ICourseRepository
    {
        public CourseRepository(IConfiguration config) : base(config)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        /// 
        public async Task<IEnumerable<Course>> GetCourses(ListingParameter listingParameter)
        {
            var countSql = @"SELECT COUNT(*) 
                     FROM dbo.Course a with (nolock) 
                     WHERE a.IsActive=0 AND a.Title like @TextToSearch";

            string sql;
            if (string.IsNullOrEmpty(listingParameter.TextToSearch))
            {
                sql = @"
            SELECT a.Id, a.Title, a.Description
            FROM dbo.Course a with (nolock)
            WHERE a.IsActive=0
            ORDER BY a.CreatedBy DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";
            }
            else
            {
                sql = @"
            SELECT a.Id, a.Title, a.Description
            FROM dbo.Course a with (nolock)
            WHERE a.IsActive=0 AND a.Title like @TextToSearch                        
            ORDER BY a.CreatedBy DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";
            }

            using (SqlConnection connection = new SqlConnection(CoddingGurrusDbConnectionString))
            {
                var parameters = new
                {
                    TextToSearch = $"%{listingParameter.TextToSearch}%", // Applying wildcard here
                    Skip = (listingParameter.Skip - 1) * listingParameter.Take, // Calculate skip based on Skip and Take
                    Take = listingParameter.Take // Use Take directly
                };

                var grid = await connection.QueryMultipleAsync(sql + countSql, parameters);
                var courses = grid.Read<CourseWithCount>().ToList();
                var totalRecords = grid.Read<int>().FirstOrDefault();

                courses.ForEach(course => course.TotalRecords = totalRecords);
                return courses;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<int> AddCourse(Course course)
        {
            string sql = @"
            INSERT INTO dbo.Course (Title, Description,IsActive)
            VALUES (@Title, @Description,0);
            SELECT SCOPE_IDENTITY();";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int courseId = await connection.ExecuteScalarAsync<int>(sql, course);

            return courseId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<bool> TitleExists(string title)
        {
            var sql = @"SELECT TOP 1 1
            FROM dbo.Course
            WHERE Title = @title"
            ;
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var results = await connection.QueryFirstOrDefaultAsync<int>(sql, new { title });

            return results > 0;
        }

        /// <summary>
        /// Update an course against its reference
        /// </summary>
        /// <param name="model">Course model</param>
        /// <returns></returns>
        public async Task<bool> UpdateCourse(Course model)
        {
            var sql = @"UPDATE Course 
                 SET Title = @Title,
                     Description = @Description
                 WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var result = await connection.ExecuteAsync(sql, new { model.Title, model.Description, model.Id });
            return result > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCourse(long Id)
        {
            var sql = @"DELETE FROM Course                         
            WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var result = await connection.ExecuteAsync(sql, new { Id });
            return result > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Course> GetCourseById(long id)
        {
            var sql = @"SELECT Id, Title, Description FROM Course WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var course = await connection.QuerySingleOrDefaultAsync<Course>(sql, new { Id = id });
            return course;
        }

    }
}
