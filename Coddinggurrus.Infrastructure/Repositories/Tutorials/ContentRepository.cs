using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials
{
    public class ContentRepository : BaseRepository, IContentRepository
    {
        public ContentRepository(IConfiguration config) : base(config)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> AddContent(Content content)
        {
            string sql = @"
            INSERT INTO dbo.Content (TopicId, Title,Text,IsActive)
            VALUES (@TopicId, @Title,@Text,0);
            SELECT SCOPE_IDENTITY();";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int contentId = await connection.ExecuteScalarAsync<int>(sql, content);

            return contentId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteContent(long Id)
        {
            var sql = @"
            UPDATE Content
            SET IsActive = 1
            WHERE Id = @Id";
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var result = await connection.ExecuteAsync(sql, new { Id });
            return result > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listingParameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Content>> GetContents(ListingParameter listingParameter)
        {
            var countSql = @"SELECT COUNT(*) 
                     FROM dbo.Content a with (nolock) 
                     WHERE a.IsActive=0 AND a.Title like @TextToSearch";

            string sql;
            if (string.IsNullOrEmpty(listingParameter.TextToSearch))
            {
                sql = @"
            SELECT a.Id, a.Title, a.Text, '' AS Topic
            FROM dbo.Content a with (nolock)
            WHERE a.IsActive=0
            ORDER BY a.CreatedBy DESC
            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";
            }
            else
            {
                sql = @"
            SELECT a.Id, a.Title, a.Text, '' AS Topic
            FROM dbo.Content a with (nolock)
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
                var contents = grid.Read<ContentWithCount>().ToList();
                var totalRecords = grid.Read<int>().FirstOrDefault();

                contents.ForEach(topic => topic.TotalRecords = totalRecords);
                return contents;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Content> GetContentById(long id)
        {
            var sql = @"SELECT Id, Title, TopicId,Text CourseId FROM Content WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var content = await connection.QuerySingleOrDefaultAsync<Content>(sql, new { Id = id });
            return content;
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
            FROM dbo.Content
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
        public async Task<bool> UpdateContent(Content model)
        {
            var sql = @"UPDATE Content 
                 SET Title = @Title,
                     TopicId = @TopicId,
                     Text = @Text
                 WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var result = await connection.ExecuteAsync(sql, new { model.Title, model.TopicId, model.Id, model.Text });
            return result > 0;
        }
    }
}
