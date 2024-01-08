using Coddinggurrus.Business.Services.User;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.Repositories.User;

namespace Coddinggurrus.Api.Extensions
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfigurationRoot config)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
