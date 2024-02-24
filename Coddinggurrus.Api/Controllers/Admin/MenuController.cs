using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Course;
using Coddinggurrus.Api.Models.Admin.Menu;

using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Services.Menus;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Coddinggurrus.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : AdminController
    {
        private readonly IMenuService _menuService;
        BasicResponse basicResponse;
        public MenuController(IMenuService menuService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _menuService = menuService;
            basicResponse = new BasicResponse();
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery]ListingParameter listingParameter)
        {
            try
            {
                var menus = await _menuService.GetMenus(listingParameter);
                basicResponse.Data = JsonConvert.SerializeObject(menus);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody]MenuModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    return BadRequest($"Missing required fields.");

                var nameExists = await _menuService.NameExists(model.Name);
                if (nameExists) return BadRequest($"Course {model.Name} already exists.");

                var menus = await _menuService.AddMenu(Mapper.Map<Menu>(model));
                basicResponse.Data = JsonConvert.SerializeObject(menus);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var menu = await _menuService.GetMenuById(Id);
            basicResponse.Data = JsonConvert.SerializeObject(menu);
            return Ok(basicResponse);
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]MenuModel model)
        {
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
