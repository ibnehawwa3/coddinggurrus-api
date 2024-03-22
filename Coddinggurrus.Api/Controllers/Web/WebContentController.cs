using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Coddinggurrus.Api.Controllers.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebContentController : ApiController
    {
        public WebContentController(IMapper mapper, IConfiguration config) : base(mapper, config)
        {
        }
    }
}
