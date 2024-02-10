
namespace Coddinggurrus.Core.Entities.User
{
    public class UserProfiles : Entity<int>
    {
        public string UserId { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public bool IsDeleted { get; set; }
        public string VerificationCode { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
    }
}
