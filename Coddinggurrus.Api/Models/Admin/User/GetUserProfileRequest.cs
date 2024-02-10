using System.ComponentModel.DataAnnotations;

namespace Coddinggurrus.Api.Models.Admin.User
{
    public class GetUserProfileRequest
    {
        [Required]
        public string Id { get; set; }
    }
}
