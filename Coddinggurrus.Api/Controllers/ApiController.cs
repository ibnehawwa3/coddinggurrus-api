using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coddinggurrus.Api.Controllers
{
    [Authorize]
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
