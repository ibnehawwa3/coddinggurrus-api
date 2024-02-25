using AutoMapper;

namespace Coddinggurrus.Api.Controllers.Admin.Tutorials
{
    public class ContentController : AdminController
    {
        public ContentController(IMapper mapper, IConfiguration config) : base(mapper, config)
        {
        }
    }
}
