using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Business.Services
{
    public class BaseService
    {
        protected IConfiguration Config { get; private set; }

        public BaseService(IConfiguration config)
        {
            Config = config;
        }
    }
}
