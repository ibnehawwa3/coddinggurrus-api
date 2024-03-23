using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Coddinggurrus.Api.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ApiController
    {
        public PagesController(IMapper mapper, IConfiguration config) : base(mapper, config)
        {
        }
    }
}
