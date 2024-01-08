CREATE PROCEDURE [dbo].[CoddingGurrus_Dev_GetUserList]
		@pageNo INT,
		@pageSize INT, 
		@searchQuery NVARCHAR(50) 
AS

BEGIN

		SELECT		U.FirstName,
					U.Email,
					U.Id,
					U.DateRegistration,
					
					TotalRecords = COUNT(*) OVER()
		FROM		AspNetUsers U
					INNER JOIN AspNetUserRoles ur on ur.UserId = u.Id
					INNER JOIN AspNetRoles R ON UR.RoleId = R.Id AND R.Name = 'User' 
		WHERE		(u.UserName like '%'+IsNull(@searchQuery,u.UserName)+'%') 
					OR (u.Email like '%'+IsNull(@searchQuery,u.Email)+'%')
					OR (u.PhoneNumber like '%'+IsNull(@searchQuery,u.PhoneNumber)+'%')
					

					ORDER BY (u.DateRegistration) DESC
					OFFSET (@pageNo-1)*@pageSize ROWS
					FETCH NEXT @pageSize ROWS ONLY

END