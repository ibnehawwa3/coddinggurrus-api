using AutoMapper;
using Coddinggurrus.Api.Models.Admin.User;
using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Core.Models.User;
using Coddinggurrus.Infrastructure.APIModels;
using Coddinggurrus.Infrastructure.Enums;
using Coddinggurrus.Infrastructure.Exceptions;
using Coddinggurrus.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Coddinggurrus.Api.Controllers.Admin
{
    public class UserProfile : AdminController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserProfile(UserManager<ApplicationUser> userManager,IUserProfileService userProfileService,IMapper mapper, IConfiguration config) : base(mapper, config)
        {
            _userManager = userManager;
            _userProfileService = userProfileService;
        }
        [HttpPost]
        [Route("get-profile")]
        public IActionResult GetProfile(GetUserProfileRequest getUserProfileRequest)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                UserProfileInformation userProfile = _userProfileService.GetUserProfileInformation(getUserProfileRequest.Id);
                basicResponse.Data = userProfile;
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        [HttpPost]
        [Route("update-profile")]
        public IActionResult UpdateUserProfile(UpdateUserProfileRequest model)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                basicResponse.Data = _userProfileService.Update(Mapper.Map<UserProfiles>(model));
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string Id)
        {
            BasicResponse basicResponse = new BasicResponse();
            try
            {
                ApplicationUser user = await this._userManager.FindByIdAsync(Id);
                if (user != null)
                {

                    user.UserName = string.Concat("anonymous", CommonHelpers.GetRandomNumber());
                    user.Email = string.Concat("anonymous", CommonHelpers.GetRandomNumber());
                    user.PhoneNumber = string.Concat("anonymous", CommonHelpers.GetRandomNumber());
                    user.NormalizedUserName = string.Concat("anonymous", CommonHelpers.GetRandomNumber());
                    await this._userManager.UpdateAsync(user);
                    basicResponse.Data = _userProfileService.DeleteUser(Id);
                }
                else
                    throw new GenericException(ErrorMessages.USER_NOT_EXIST);
            }
            catch (Exception e)
            {
                basicResponse.ErrorMessage = e.Message;
            }
            return Ok(basicResponse);
        }
    }
}
