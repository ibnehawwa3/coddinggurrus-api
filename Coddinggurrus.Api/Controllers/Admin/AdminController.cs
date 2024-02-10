using AutoMapper;

namespace Coddinggurrus.Api.Controllers.Admin
{
    public class AdminController : ApiController
    {
        public AdminController(IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            
        }
    }
}
