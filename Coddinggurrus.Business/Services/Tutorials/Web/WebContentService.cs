using AutoMapper;
using Coddinggurrus.Core.Dto.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials.Web
{
    public class WebContentService : BaseService, IWebContentService
    {
        private readonly IWebContentRepository _webContentRepository;
        public WebContentService(IWebContentRepository webContentRepository, IConfiguration config, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _webContentRepository = webContentRepository;
        }

    }
}
