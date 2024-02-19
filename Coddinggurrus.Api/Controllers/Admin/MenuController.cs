using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Course;
using Coddinggurrus.Api.Models.Admin.Menu;
using Coddinggurrus.Business.Services.Tutorials;
using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.MenuRepo;
using Coddinggurrus.Core.Interfaces.Services.Menus;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;


namespace Coddinggurrus.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : AdminController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _menuService = menuService;
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery]ListingParameter listingParameter)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                var users = await _menuService.GetMenus(listingParameter);
                basicResponse.Data = users;
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody]MenuModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    return BadRequest($"Missing required fields.");

                var nameExists = await _menuService.NameExists(model.Name);
                if (nameExists) return BadRequest($"Course {model.Name} already exists.");

                var users = await _menuService.AddMenu(Mapper.Map<Menu>(model));
                basicResponse.Data = users;
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCourse([FromBody]MenuModel model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    return BadRequest($"Missing required fields.");

                await _menuService.UpdateMenu(Mapper.Map<Menu>(model));
                basicResponse.Data = NoContent();
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(long Id)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                await _menuService.DeleteMenu(Id);
                basicResponse.Data = NoContent();
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
    }
}
