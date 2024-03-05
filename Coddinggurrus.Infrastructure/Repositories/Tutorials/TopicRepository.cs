using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.Models.Generic;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials
{
    public class TopicRepository : BaseRepository, ITopicRepository
    {
        public TopicRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> AddTopic(Topic topic)
        {
            topic.CreatedBy = CreatedBy;
            string sql = @"
            INSERT INTO dbo.Topic (Title, Description,CourseId,IsActive)
            VALUES (@Title, @Description,@CourseId,0);
            SELECT SCOPE_IDENTITY();";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int topicId = await connection.ExecuteScalarAsync<int>(sql, topic);

            return topicId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteTopic(long Id)
        {
            var sql = @"
            UPDATE Topic
            SET IsActive = 1
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
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Topic> GetTopicById(long id)
        {
            var sql = @"SELECT Id, Title, Description, CourseId FROM Topic WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var topic = await connection.QuerySingleOrDefaultAsync<Topic>(sql, new { Id = id });
            return topic;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listingParameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<TopicCount>> GetTopics(ListingParameter listingParameter)
        {
            var countSql = @"SELECT COUNT(*) 
                     FROM dbo.Topic a with (nolock) 
                     WHERE a.IsActive=0 AND a.Title like @TextToSearch";

            string sql;
            if (string.IsNullOrEmpty(listingParameter.TextToSearch))
            {
                sql = @"
            SELECT a.Id, a.Title, a.Description
            FROM dbo.Topic a with (nolock)
            WHERE a.IsActive=0
            ORDER BY a.CreatedBy DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";
            }
            else
            {
                sql = @"
            SELECT a.Id, a.Title, a.Description
            FROM dbo.Topic a with (nolock)
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
                var topics = grid.Read<TopicCount>().ToList();
                var totalRecords = grid.Read<int>().FirstOrDefault();

                topics.ForEach(topic => topic.TotalRecords = totalRecords);
                return topics;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<DropdownListItems>> GetTopicsByCourseId(long courseId)
        {
            var sql = @"
        SELECT Id, Title AS Name
        FROM dbo.Topic with (nolock)
        WHERE IsActive = 0 AND CourseId= @courseId
        ORDER BY CreatedBy DESC
        ";

            using (SqlConnection connection = new SqlConnection(CoddingGurrusDbConnectionString))
            {
                var items = await connection.QueryAsync<DropdownListItems>(sql, new { courseId });
                return items;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> TitleExists(string title)
        {
            var sql = @"SELECT TOP 1 1
            FROM dbo.Topic
            WHERE Title = @title"
            ;
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var results = await connection.QueryFirstOrDefaultAsync<int>(sql, new { title });

            return results > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateTopic(Topic model)
        {
            var sql = @"UPDATE Topic 
                 SET Title = @Title,
                     Description = @Description,
                     CourseId = @CourseId
                 WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var result = await connection.ExecuteAsync(sql, new { model.Title, model.Description, model.Id, model.CourseId });
            return result > 0;
        }
    }
}
