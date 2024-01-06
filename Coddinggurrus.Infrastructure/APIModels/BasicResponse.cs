using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Infrastructure.APIModels
{
    public class BasicResponse
    {
        public BasicResponse()
        {
            this.Success = true;
            this.ErrorMessage = string.Empty;
        }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public dynamic Data { get; set; }
    }
}
