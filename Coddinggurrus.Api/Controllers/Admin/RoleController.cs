using AutoMapper;
using Coddinggurrus.Core.Entities.Role;
using Coddinggurrus.Infrastructure.APIModels;
using Coddinggurrus.Infrastructure.Enums;
using Coddinggurrus.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Coddinggurrus.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private BasicResponse basicResponse;

        public RoleController(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            basicResponse = new BasicResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var roles =await _roleManager.Roles.ToListAsync();
                basicResponse.Data = JsonConvert.SerializeObject(roles);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            var roleById =await _roleManager.Roles.FirstOrDefaultAsync(s=>s.Id==Id);
            basicResponse.Data=JsonConvert.SerializeObject(roleById);
            return Ok(basicResponse);
        }

        [HttpPost("PostRole")]
        public async Task<IActionResult> Post(ApplicationRole Role)
        {
            IdentityResult roleResult = await this._roleManager.CreateAsync(Role);
            if (roleResult.Succeeded)
            {
                basicResponse.Success = true;
            }
            else
                throw new GenericException(ErrorMessages.USER_NOT_ADDED_TO_ROLE);

            return Ok(basicResponse);
        }

        [HttpPost("Put")]
        public async Task<IActionResult> Put(ApplicationRole role)
        {
            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(role.Id);

            if (applicationRole != null)
            {
                applicationRole.Name = role.Name;
                applicationRole.NormalizedName = role.Name;
                IdentityResult result = await _roleManager.UpdateAsync(applicationRole);

                if (result.Succeeded)
                {
                    basicResponse.Success = true;
                }
                else
                {
                    basicResponse.Success = false;
                }
            }

            return Ok(basicResponse);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);

            if (applicationRole != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(applicationRole);

                if (result.Succeeded)
                {
                    basicResponse.Success = true;
                    return Ok(basicResponse);
                }
                else
                {
                    basicResponse.Success = false;
                }
            }

            return Ok(basicResponse);
        }
    }
}
