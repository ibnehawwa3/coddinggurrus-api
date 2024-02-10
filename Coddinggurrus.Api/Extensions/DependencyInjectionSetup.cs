using Coddinggurrus.Business.Services.Tutorials;
using Coddinggurrus.Business.Services.User;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.Repositories.Tutorials;
using Coddinggurrus.Infrastructure.Repositories.User;

namespace Coddinggurrus.Api.Extensions
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfigurationRoot config)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserProfileService, UserProfileService>()
                .AddScoped<ICourseService, CourseService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserProfileRepository, UserProfileRepository>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
