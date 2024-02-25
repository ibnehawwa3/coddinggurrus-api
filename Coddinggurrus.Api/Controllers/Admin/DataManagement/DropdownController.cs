using AutoMapper;

namespace Coddinggurrus.Api.Controllers.Admin.DataManagement
{
    public class DropdownController : AdminController
    {
        public DropdownController(IMapper mapper, IConfiguration config) : base(mapper, config)
        {
        }
    }
}
