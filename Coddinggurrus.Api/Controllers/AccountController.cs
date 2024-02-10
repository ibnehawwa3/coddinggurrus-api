using Coddinggurrus.Core.Entities.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.APIModels;
using Coddinggurrus.Infrastructure.APIRequestModels.User;
using Coddinggurrus.Infrastructure.Enums;
using Coddinggurrus.Infrastructure.Exceptions;
using Coddinggurrus.Infrastructure.Helpers;
using Coddinggurrus.Infrastructure.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Coddinggurrus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IConfiguration _configuration;
        private readonly IUserProfileService _userProfileService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IUserProfileService userProfileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _configuration = configuration;
            _userProfileService = userProfileService;
        }
        #region Register new User
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest registerRequest)
        {
            BasicResponse basicResponse = new BasicResponse();
            ApplicationUser dbUser = await this._userManager.FindByNameAsync(registerRequest.Email);
            if (dbUser == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = registerRequest.Email,
                    Email = registerRequest.Email,
                    DateRegistration = DateTime.UtcNow,
                    EmailConfirmed = true
                };
                IdentityResult result = await this._userManager.CreateAsync(user, registerRequest.Password);
                if (result.Succeeded)
                {
                    IdentityResult roleResult = await this._userManager.AddToRoleAsync(user, UserRoles.User.ToString());
                    if (roleResult.Succeeded)
                    {
                        ApplicationUser newUser = await this._userManager.FindByEmailAsync(registerRequest.Email);
                        try
                        {
                            bool r = _userProfileService.AddProfile(registerRequest.FirstName, registerRequest.Email, newUser.Id);
                            if (r)
                            {
                                basicResponse.Data = newUser.UserName;
                            }
                        }
                        catch (Exception ex)
                        {
                            basicResponse.ErrorMessage = ex.Message;
                        }
                    }
                    else
                        throw new GenericException(ErrorMessages.USER_NOT_ADDED_TO_ROLE);
                }
                else
                    basicResponse.ErrorMessage = ResponseMessage.USER_NOT_CREATED.ToString();
            }
            else
                basicResponse.ErrorMessage = ResponseMessage.USER_ALREADY_EXIST.ToString();

            return Ok(basicResponse);
        }
        #endregion
        #region login
        [HttpPost]
        [Route("/api/account/login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            BasicResponse basicResponse = new BasicResponse();
            ApplicationUser user = await this._userManager.FindByEmailAsync(loginRequest.Email);
            try
            {
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
                    if (result.Succeeded)
                    {
                        basicResponse.Data = _GenerateJSONWebToken(user, UserRoles.User.ToString());
                    }
                    else if (result.IsLockedOut)
                    {
                        basicResponse.ErrorMessage = ResponseMessage.USER_IS_DISABLED.ToString();
                    }
                    else
                    {
                        basicResponse.ErrorMessage = ResponseMessage.USER_INVALID_USERNAME_PASSWORD.ToString();
                    }
                }
                else
                {
                    basicResponse.ErrorMessage = ResponseMessage.USER_INVALID_USERNAME_PASSWORD.ToString();
                }
            }
            catch (Exception ex)
            {
                basicResponse.ErrorMessage = ex.Message;
            }
            return Ok(basicResponse);
        }
        #endregion login
        #region token generate
        private TokenResponse _GenerateJSONWebToken(ApplicationUser applicationUser, string role)
        {
            applicationUser.Email = "";
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:JwtKey")));
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, applicationUser.UserName),
            new Claim(ClaimTypes.Email, applicationUser.UserName),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.NameIdentifier , applicationUser.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetValue<string>("AppSettings:JwtExpireTime"))),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration.GetValue<string>("AppSettings:JwtIssuer"),
                Audience = _configuration.GetValue<string>("AppSettings:JwtIssuer")
            };

            SecurityToken jwtToken = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            TokenResponse tokenResponse = new TokenResponse()
            {
                auth_token = token,
                expiration_time = jwtToken.ValidTo.ToString(Constants.MobileDateTimeFormat),
                issue_time = jwtToken.ValidFrom.ToString(Constants.MobileDateTimeFormat),
                role = role
            };
            return tokenResponse;
        }
        #endregion
    }
}
