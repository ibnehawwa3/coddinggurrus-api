using Coddinggurrus.Business.Services.Course;
using Coddinggurrus.Business.Services.User;
using Coddinggurrus.Core.Interfaces.Repositories.Course;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.Course;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.Repositories.Course;
using Coddinggurrus.Infrastructure.Repositories.User;

namespace Coddinggurrus.Api.Extensions
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfigurationRoot config)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<ICourseService, CourseService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
