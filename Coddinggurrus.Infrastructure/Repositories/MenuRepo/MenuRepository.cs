using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.MenuRepo;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;


namespace Coddinggurrus.Infrastructure.Repositories.MenuRepo
{
    public class MenuRepository : BaseRepository , IMenuRepository
    {

        public MenuRepository(IConfiguration config) : base(config)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Menu>> GetMenus(ListingParameter listingParameter)
        {
            var countSql = @$"SELECT COUNT(*) 
                     FROM dbo.Menus a with (nolock) 
                     WHERE a.Name like '%{listingParameter.TextToSearch}%'";
            string sql;
            if (string.IsNullOrEmpty(listingParameter.TextToSearch))
            {
                sql = @$"SELECT a.Id, a.Name,Url, a.MenuOrder,a.MenuImage, a.IsShow
             FROM dbo.Menus a with (nolock)                       
             ORDER BY a.CreatedBy desc                        
             OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY
             {countSql}";
            }
            else
            {
                sql = @$"SELECT a.Id, a.Name,Url, a.MenuOrder,a.MenuImage, a.IsShow
             FROM dbo.Menus a with (nolock)
             WHERE a.Name like '%{listingParameter.TextToSearch}%'                        
             ORDER BY a.CreatedBy desc
             OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY
             {countSql}";
            }
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var grid = await connection.QueryMultipleAsync(sql, new { listingParameter.TextToSearch, listingParameter.Skip, listingParameter.Take });
            var articles = grid.Read<MenuWithCount>().ToList();
            var TotalCount = grid.Read<int>().FirstOrDefault();
            articles.ForEach(article => article.TotalCount = TotalCount);

            grid.Dispose();
            return articles;
        }

        public async Task<Menu> GetMenuById(int id)
        {
            var sql = @$"SELECT a.Id, a.Name,Url, a.MenuOrder,a.MenuImage, a.IsShow
             FROM dbo.Menus a with (nolock)
             WHERE a.Id={id}";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var grid = await connection.QueryMultipleAsync(sql, new { id });
            var articles = grid.Read<Menu>().FirstOrDefault();

            grid.Dispose();
            return articles;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<int> AddMenu(Menu menu)
        {
            string sql = @"
            INSERT INTO dbo.Menus (a.Name, a.Url, a.MenuOrder,a.MenuImage, a.IsShow)
            VALUES (@Name, @Url,@MenuOrder, @MenuImage,@IsShow);
            SELECT SCOPE_IDENTITY();";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int courseId = await connection.ExecuteScalarAsync<int>(sql, menu);

            return courseId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<bool> NameExists(string name)
        {
            var sql = @"SELECT TOP 1 Id FROM dbo.Menus WHERE Name = @name";
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var results = await connection.QueryFirstOrDefaultAsync<int>(sql, new { name });

            return results > 0;
        }

        /// <summary>
        /// Update an course against its reference
        /// </summary>
        /// <param name="model">Course model</param>
        /// <returns></returns>
        public async Task<bool> UpdateMenu(Menu model)
        {
            var sqlQuery = @"UPDATE Menus 
                 SET Name = @Name,
                     Url = @Url,
                     Archived=@Archived,
                     MenuOrder = @MenuOrder,
                     ParentId=@ParentId,
                     MenuImage = @MenuImage,
                     IsShow = @IsShow
                 WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int result = connection.Execute(sqlQuery, new
            {
                Name = model.Name,
                Url = model.Url,
                Archived = model.Archived,
                MenuOrder = model.MenuOrder,
                ParentId = model.ParentId,
                MenuImage = model.MenuImage,
                IsShow = model.IsShow,
                Id = model.Id
            });
            return result > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMenu(long Id)
        {
            var sql = @"DELETE FROM Menus                         
            WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var result = await connection.ExecuteAsync(sql, new { Id });
            return result > 0;
        }
    }
}
