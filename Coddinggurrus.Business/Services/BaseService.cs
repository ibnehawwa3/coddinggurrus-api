using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services
{
    public class BaseService
    {
        protected IConfiguration Config { get; private set; }
        protected readonly IMapper Mapper;
        protected readonly IMemoryCache Cache;

        public BaseService(IConfiguration config, IMapper mapper, IMemoryCache cache)
        {
            Config = config;
            Mapper = mapper;
            Cache = cache;
        }
    }
}
