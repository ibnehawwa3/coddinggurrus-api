using Coddinggurrus.Business.Services.Menus;
using Coddinggurrus.Business.Services.RoleMenuPermissions;
using Coddinggurrus.Business.Services.Tutorials;
using Coddinggurrus.Business.Services.User;
using Coddinggurrus.Core.Interfaces.Repositories.MenuRepo;
using Coddinggurrus.Core.Interfaces.Repositories.RoleMenuPermissions;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.Menus;
using Coddinggurrus.Core.Interfaces.Services.RoleMenuPermissions;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.Repositories.MenuRepo;
using Coddinggurrus.Infrastructure.Repositories.RoleMenuPermissions;
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
                .AddScoped<IRoleMenuPermissionService, RoleMenuPermissionService>()
                .AddScoped<IMenuService, MenuService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserProfileRepository, UserProfileRepository>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<IMenuRepository, MenuRepository>()
                .AddScoped<IRoleMenuPermissionRepositry, RoleMenuPermissionRepositry>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
