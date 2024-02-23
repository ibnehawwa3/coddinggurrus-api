using AutoMapper;

namespace Coddinggurrus.Api.Controllers.Admin.DataManagement
{
    public class DataController : AdminController
    {
        public DataController(IMapper mapper, IConfiguration config) : base(mapper, config)
        {
        }
    }
}
