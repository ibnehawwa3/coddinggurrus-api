using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials
{
    public class TopicRepository : BaseRepository, ITopicRepository
    {
        public TopicRepository(IConfiguration config) : base(config)
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
            string sql = @"
            INSERT INTO dbo.Topic (Title, Description,CourseId)
            VALUES (@Title, @Description,@CourseId);
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
            SET IsActive = 0
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
            var sql = @"SELECT Id, Title, Description FROM Topic WHERE Id = @Id";

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
        public async Task<IEnumerable<Topic>> GetTopics(ListingParameter listingParameter)
        {
            var countSql = @$"SELECT COUNT(*) 
                     FROM dbo.Topic a with (nolock) 
                     WHERE a.Title like '%{listingParameter.TextToSearch}%'";
            string sql;
            if (string.IsNullOrEmpty(listingParameter.TextToSearch))
            {
                sql = @$"SELECT a.Id, a.Title, a.Description
             FROM dbo.Topic a with (nolock)                       
             ORDER BY a.CreatedBy desc                        
             OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY
             {countSql}";
            }
            else
            {
                sql = @$"SELECT a.Id, a.Title, a.Description
             FROM dbo.Topic a with (nolock)
             WHERE a.Title like '%{listingParameter.TextToSearch}%'                        
             ORDER BY a.CreatedBy desc
             OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY
             {countSql}";
            }
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var grid = await connection.QueryMultipleAsync(sql, new { listingParameter.TextToSearch, listingParameter.Skip, listingParameter.Take });
            var topics = grid.Read<TopicCount>().ToList();
            var TotalRecords = grid.Read<int>().FirstOrDefault();
            topics.ForEach(article => article.TotalRecords = TotalRecords);

            grid.Dispose();
            return topics;
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
            var result = await connection.ExecuteAsync(sql, new { model.Title, model.Description, model.Id });
            return result > 0;
        }
    }
}
