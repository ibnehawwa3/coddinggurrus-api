using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Coddinggurrus.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected IConfiguration Config;
        public ApiController(IMapper mapper, IConfiguration config)
        {
            Mapper = mapper;
            Config = config;
        }
    }
}
