using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services
{
    public class BaseService
    {
        protected IConfiguration Config { get; private set; }
        protected readonly IMapper Mapper;

        public BaseService(IConfiguration config, IMapper mapper)
        {
            Config = config;
            Mapper = mapper;
        }
    }
}
