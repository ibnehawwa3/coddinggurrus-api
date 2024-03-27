using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials
{
    public class ContentRepository : BaseRepository, IContentRepository
    {
        public ContentRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
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
            content.CreatedBy = CreatedBy;
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
                     WHERE a.IsActive=0 AND (@TextToSearch IS NULL OR a.Title like '%' + @TextToSearch + '%')";

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
        public async Task<ContentViewModel> GetContentById(long id)
        {
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            

            var sql = @"SELECT Id, Title, TopicId,Text FROM Content WHERE Id = @Id";

            var content = await connection.QuerySingleOrDefaultAsync<Content>(sql, new { Id = id });

            var sqlQuery = @"Select CourseId from Topic where Id= @Id";

            var topic = await connection.QuerySingleOrDefaultAsync(sqlQuery, new { Id = content.TopicId });

            ContentViewModel contentViewModel = new ContentViewModel
            {
                Id = content.Id,
                Title = content.Title,
                Text = content.Text,
                TopicId = content.TopicId,
                CourseId = topic.CourseId
            };
            return contentViewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>b   
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> TitleExists(string title, long topicId)
        {
            var sql = @"SELECT TOP 1 1
                FROM dbo.Content
                WHERE Title = @title AND TopicId = @topicId";

            using SqlConnection connection = new SqlConnection(CoddingGurrusDbConnectionString);
            var results = await connection.QueryFirstOrDefaultAsync<int>(sql, new { title, topicId });

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
