using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Infrastructure.APIModels
{
    public class TokenResponse
    {
        public TokenResponse()
        {
            this.token_type = "bearer";
        }
        public string auth_token { get; set; }
        public string token_type { get; set; }
        public string issue_time { get; set; }
        public string expiration_time { get; set; }
        public string role { get; set; }
    }
}
