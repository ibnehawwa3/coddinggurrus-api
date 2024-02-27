using AutoMapper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;

namespace Coddinggurrus.Api.Controllers.Admin.Tutorials
{
    public class ContentController : AdminController
    {
        private readonly IContentService _contentService;
        public ContentController(IContentService contentService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _contentService = contentService;
        }
    }
}
