using AutoMapper;
using Coddinggurrus.Business.Services.User;
using Coddinggurrus.Core.Entities.Role;
using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.APIModels;
using Coddinggurrus.Infrastructure.APIRequestModels.User;
using Coddinggurrus.Infrastructure.Enums;
using Coddinggurrus.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

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
        public Task<List<ApplicationRole>> Get()
        {
            return basicResponse.Data=_roleManager.Roles.ToListAsync();
        }


        [HttpGet("{id}")]
        public Task<ApplicationRole> GetById()
        {
            return basicResponse.Data = _roleManager.Roles.FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string RoleName)
        {
            ApplicationRole appicationRole = new ApplicationRole
            {
                Name = RoleName,
                Id=RoleName
            };
            IdentityResult roleResult = await this._roleManager.CreateAsync(appicationRole);
            if (roleResult.Succeeded)
            {
                basicResponse.Success = true;
            }
            else
                throw new GenericException(ErrorMessages.USER_NOT_ADDED_TO_ROLE);

            return Ok(basicResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id , string RoleName)
        {
            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);

            if (applicationRole != null)
            {
                applicationRole.Name = RoleName;
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

        [HttpDelete("{id}")]
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
