
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Coddinggurrus.Core.Helper
{
    public static class HttpContextExtensionMethods
    {
        /// <summary>
        /// Get userId from the claim.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static string GetCurrentUserId(this IHttpContextAccessor httpContextAccessor)
        {
            string userId = string.Empty;
            HttpContext context = httpContextAccessor.HttpContext;
            if (context != null)
            {
                bool hasUserId = httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier);
                if (hasUserId)
                {
                    userId = httpContextAccessor?.HttpContext?.User?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? null;
                }
            }
            return userId;
        }

        /// <summary>
        /// Get user email from the claim.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static string GetCurrentUserEmail(this IHttpContextAccessor httpContextAccessor)
        {
            string userEmail = string.Empty;
            HttpContext context = httpContextAccessor.HttpContext;
            if (context != null)
            {
                bool hasUserEmail = httpContextAccessor.HttpContext.User.HasClaim(x => x.Type == ClaimTypes.Email);
                if (hasUserEmail)
                {
                    userEmail = httpContextAccessor?.HttpContext?.User?.FindFirst(x => x.Type == ClaimTypes.Email)?.Value ?? null;
                }
            }
            return userEmail;
        }

        public static string GetCurrentRequest(this IHttpContextAccessor httpContextAccessor)
        {
            var path = httpContextAccessor.HttpContext.Request.Path;
            return path;
        }
    }
}
