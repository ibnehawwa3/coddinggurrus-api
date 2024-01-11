using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coddinggurrus.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList(int pageNo, int pageSize, string searchText = "")
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var users = await _userService.GetList(pageNo, pageSize, searchText);
                basicResponse.Data = users;
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
    }
}
