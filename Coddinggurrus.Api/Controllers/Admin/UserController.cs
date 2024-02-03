using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Core.Models.User;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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
        public async Task<IActionResult> GetList([FromQuery]ListingParameter listingParameter)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var users = await _userService.GetList(listingParameter);
                basicResponse.Data = JsonConvert.SerializeObject(users);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
    }
}
