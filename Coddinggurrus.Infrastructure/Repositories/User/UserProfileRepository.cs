using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.User
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration config) : base(config)
        {
        }

        public bool Add(UserProfiles userProfile)
        {
            bool IsAdded = false;
            const string sqlQuery = @"INSERT INTO UserProfiles (UserId, EmailAddress, MobileNumber, FirstName, LastName, StreetNumber, ZipCode, Town, Country, CountryCode, IsDeleted, VerificationCode, UpdatedBy, CreatedOn, UpdatedOn)
                             VALUES (@UserId, @EmailAddress, @MobileNumber, @FirstName, @LastName, @StreetNumber, @ZipCode, @Town, @Country, @CountryCode, @IsDeleted, @VerificationCode, @UpdatedBy, @CreatedOn, @UpdatedOn)";
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int result = connection.Execute(sqlQuery, new
            {
                UserId = userProfile.UserId,
                EmailAddress = userProfile.EmailAddress,
                MobileNumber = userProfile.MobileNumber,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                StreetNumber = userProfile.StreetNumber,
                ZipCode = userProfile.ZipCode,
                Town = userProfile.Town,
                Country = userProfile.Country,
                CountryCode = userProfile.CountryCode,
                IsDeleted = userProfile.IsDeleted,
                VerificationCode = userProfile.VerificationCode,
                UpdatedBy = userProfile.UpdatedBy,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            });
            IsAdded = result > 0;
            return IsAdded;
        }

        public bool Delete(string Id)
        {
            const string sqlQuery = @"UPDATE UserProfiles SET IsDeleted = @IsDeleted WHERE UserId = @Id";
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int result = connection.Execute(sqlQuery, new
            {
                IsDeleted = true,
                Id = Id
            });
            return result > 0;
        }

        public UserProfiles GetByUserId(string userId)
        {
            const string sqlQuery = @"SELECT * FROM UserProfiles WHERE UserId = @UserId";
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            return connection.QueryFirstOrDefault<UserProfiles>(sqlQuery, new { UserId = userId });
        }


        public bool Update(UserProfiles userProfiles)
        {
            const string sqlQuery = @"UPDATE UserProfiles SET 
                                VerificationCode = @VerificationCode, 
                                EmailAddress = @EmailAddress,
                                FirstName = @FirstName,
                                LastName = @LastName,
                                StreetNumber = @StreetNumber,
                                ZipCode = @ZipCode,
                                Town = @Town,
                                Country = @Country,                                        
                                UpdatedBy = @UpdatedBy,
                                UpdatedOn = @UpdatedOn,
                                MobileNumber = @MobileNumber,
                                CountryCode=@CountryCode
                             WHERE Id = @Id";
            using SqlConnection connection = new(CoddingGurrusDbConnectionString);
            int result = connection.Execute(sqlQuery, new
            {
                VerificationCode = userProfiles.VerificationCode,
                EmailAddress = userProfiles.EmailAddress,
                FirstName = userProfiles.FirstName,
                LastName = userProfiles.LastName,
                StreetNumber = userProfiles.StreetNumber,
                ZipCode = userProfiles.ZipCode,
                Town = userProfiles.Town,
                Country = userProfiles.Country,
                UpdatedBy = userProfiles.UpdatedBy,
                UpdatedOn = userProfiles.UpdatedOn,
                MobileNumber = userProfiles.MobileNumber,
                CountryCode = userProfiles.CountryCode,
                Id = userProfiles.Id
            });
            return result > 0;
        }
    }
}
