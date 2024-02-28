using AutoMapper;
using Coddinggurrus.Api.Models.Admin.RoleMenuPermission;
using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Interfaces.Services.RoleMenuPermissions;
using Coddinggurrus.Infrastructure.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Coddinggurrus.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleMenuPermissionController : AdminController
    {
        private readonly IRoleMenuPermissionService _roleMenuPermissionService;
        private BasicResponse basicResponse;
        public RoleMenuPermissionController(IRoleMenuPermissionService roleMenuPermissionService, IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _roleMenuPermissionService = roleMenuPermissionService;
            basicResponse = new BasicResponse();
        }


        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery] string RoleId)
        {
            try
            {
                var roleMenuPermissions = await _roleMenuPermissionService.GetRoleMenuPermission(RoleId);
                basicResponse.Data = JsonConvert.SerializeObject(roleMenuPermissions);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] List<RoleMenuPermissionModel> model)
        {
            try
            {
                var roleMenuPermissions = await _roleMenuPermissionService.AddRoleMenuPermission(Mapper.Map<List<RoleMenuPermission>>(model));
                basicResponse.Data = roleMenuPermissions;
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCourse([FromBody] RoleMenuPermissionModel model)
        {
            try
            {
                await _roleMenuPermissionService.UpdateRoleMenuPermission(Mapper.Map<RoleMenuPermission>(model));
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
                await _roleMenuPermissionService.DeleteRoleMenuPermission(Id);
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
