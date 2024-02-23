using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.MenuRepo;
using Coddinggurrus.Core.Interfaces.Repositories.RoleMenuPermissions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Infrastructure.Repositories.RoleMenuPermissions
{
    public class RoleMenuPermissionRepositry : BaseRepository, IRoleMenuPermissionRepositry
    {

        public RoleMenuPermissionRepositry(IConfiguration config) : base(config)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleMenuPermission>> GetRoleMenuPermission(ListingParameter listingParameter)
        {
            //var countSql = @$"SELECT COUNT(*) 
            //         FROM dbo.RoleMenuPermissions a with (nolock) 
            //         WHERE a.Name like '%{listingParameter.TextToSearch}%'";
            string sql;
           
                sql = @$"SELECT a.Id, a.RoleId,a.MenuId, a.[Add],a.[Update], a.[Delete],a.[Access]
             FROM dbo.RoleMenuPermissions a with (nolock)                       
             ORDER BY a.CreatedBy desc                        
             OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY";
           
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var grid = await connection.QueryMultipleAsync(sql, new { listingParameter.TextToSearch, listingParameter.Skip, listingParameter.Take });
            var articles = grid.Read<RoleMenuPermissionWithCount>().ToList();
            var TotalCount = grid.Read<int>().FirstOrDefault();
            articles.ForEach(article => article.TotalCount = TotalCount);

            grid.Dispose();
            return articles;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<int> AddRoleMenuPermission(RoleMenuPermission roleMenuPermission)
        {
            string sql = @"
            INSERT INTO dbo.RoleMenuPermissions (a.RoleId,a.MenuId, a.[Add],a.[Update], a.[Delete],a.[Access])
            VALUES (@RoleId,@MenuId, @Add,@Update,@Delete,@Access);
            SELECT SCOPE_IDENTITY();";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int courseId = await connection.ExecuteScalarAsync<int>(sql, roleMenuPermission);

            return courseId;
        }

        /// <summary>
        /// Update an course against its reference
        /// </summary>
        /// <param name="model">Course model</param>
        /// <returns></returns>
        public async Task<bool> UpdateRoleMenuPermission(RoleMenuPermission model)
        {
            var sql = @"UPDATE RoleMenuPermissions 
                 SET RoleId = @RoleId,
                     MenuId = @MenuId,
                     [Add] = @Add,
                     [Update] = @Update,
                     [Delete] = @Delete,
                     [Access] = @Access,
                 WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var result = await connection.ExecuteAsync(sql, new { model.RoleId, model.MenuId, model.Access, model.Add, model.Update, model.Delete, model.Id });
            return result > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleMenuPermission(long Id)
        {
            var sql = @"DELETE FROM RoleMenuPermissions                         
            WHERE Id = @Id";

            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var result = await connection.ExecuteAsync(sql, new { Id });
            return result > 0;
        }
    }
}
