using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials.Web
{
    public class CourseContentRepository : BaseRepository, ICourseContentRepository
    {
        public CourseContentRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public async Task<Topic?> GetTopicContentById(long topicId)
        {
            var sql = @"
            SELECT t.Id, t.Title, t.Description, t.CourseId, t.Tags,
                   c.Id, c.Text , c.Title, c.TopicId
            FROM dbo.Topic t with (nolock)
            INNER JOIN dbo.Content c with (nolock) ON t.Id = c.TopicId
            WHERE t.IsActive = 0 AND t.Id = @topicId";

            using var connection = new SqlConnection(CoddingGurrusDbConnectionString);

            var results = await connection.QueryAsync<Topic, Content, Topic>(sql, map: (t, c) =>
            {
                t.Content = c;
                return t;
            }, new { topicId });

            return results.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Course>?> GetTopicsByCourseId(long courseId)
        {
            var sql = @"
            SELECT c.Id, c.Title, t.Id, t.Title
            FROM dbo.Course c with (nolock)
            INNER JOIN dbo.Topic t with (nolock) ON c.Id = t.CourseId
            WHERE c.IsActive = 0 AND c.Id = @courseId
            ORDER BY t.CreatedBy DESC";

            var meetingDictionary = new Dictionary<int, Course>();

            using var connection = new SqlConnection(CoddingGurrusDbConnectionString);

            var results = await connection.QueryAsync<Course, Topic, Course>(sql, map: (c, t) =>
            {
                if (!meetingDictionary.TryGetValue(c.Id, out var course))
                {
                    course = new Course
                    {
                        Id = c.Id,
                        Title = c.Title
                    };

                    meetingDictionary.Add(course.Id, course);
                }

                if (!course.Topics.Any(x => x.Id == t.Id))
                {
                    course.Topics.Add(t);
                }

                return course;
            }, new { courseId });

            return results.Distinct().ToList();
        }

    }
}
