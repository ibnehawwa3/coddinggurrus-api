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
        public async Task<IEnumerable<RoleMenuPermission>> GetRoleMenuPermission(string RoleId)
        {
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            var countSql = @$"SELECT COUNT(*) 
                     FROM dbo.RoleMenuPermissions a with (nolock) 
                     WHERE a.RoleId = '{RoleId}'";

            var rolegrid = await connection.QueryMultipleAsync(countSql, new { RoleId });
            var rolexist = rolegrid.Read<int>().FirstOrDefault();

            string sql=string.Empty;

            if (rolexist > 0)
            {
                sql = @$"SELECT a.Id,m.Name as MenuName,m.Id as MenuId, ISNULL(a.[Add],0) as [Add],ISNULL(a.[Update],0) 
              as [Update],ISNULL(a.[Delete],0) as [Delete],ISNULL(a.[Access],0) as [Access]
             FROM dbo.RoleMenuPermissions a with (nolock) 
			 right join dbo.Menus m on m.Id=a.MenuId
             where a.RoleId='{RoleId}'
             ORDER BY a.CreatedBy desc ";

            }
            else
            {
                sql = $@" SELECT m.Name as MenuName,m.Id as MenuId, 0 as [Add],0 as [Update],
			 0 as [Delete],0 as [Access] From
			 Menus M ";
            }
            var grid = await connection.QueryMultipleAsync(sql, new { RoleId });
            var articles = grid.Read<RoleMenuPermissionWithCount>().ToList();
            //var TotalCount = grid.Read<int>().FirstOrDefault();
            //articles.ForEach(article => article.TotalCount = TotalCount);

            grid.Dispose();
            return articles;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<int> AddRoleMenuPermission(List<RoleMenuPermission> roleMenuPermissions)
        {
            await DeleteRoleMenuPermissions(roleMenuPermissions.FirstOrDefault().RoleId);

            int courseId = 0;
            foreach (var permission in roleMenuPermissions)
            {
                string sql = @"
            INSERT INTO dbo.RoleMenuPermissions (RoleId, MenuId, [Add], [Update], [Delete], [Access])
            VALUES (@RoleId, @MenuId, @Add, @Update, @Delete, @Access);
            SELECT SCOPE_IDENTITY();";

                using SqlConnection connection = new(CoddingGurrusDbConnectionString);
                courseId = await connection.ExecuteScalarAsync<int>(sql, permission);
            }

            return courseId;
        }

        private async Task DeleteRoleMenuPermissions(string roleId)
        {
            string deleteSql = "DELETE FROM dbo.RoleMenuPermissions WHERE RoleId = @RoleId;";
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            await connection.ExecuteAsync(deleteSql, new { RoleId = roleId });
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
