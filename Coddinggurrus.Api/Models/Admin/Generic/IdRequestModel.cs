using System.ComponentModel.DataAnnotations;

namespace Coddinggurrus.Api.Models.Admin.Generic
{
    public class IdRequestModel
    {
        [Required]
        public string Id { get; set; }
    }
}
