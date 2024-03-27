using Coddinggurrus.Business.Services.Menus;
using Coddinggurrus.Business.Services.RoleMenuPermissions;
using Coddinggurrus.Business.Services.Tutorials;
using Coddinggurrus.Business.Services.Tutorials.ProblemsFaced;
using Coddinggurrus.Business.Services.Tutorials.Web;
using Coddinggurrus.Business.Services.User;
using Coddinggurrus.Core.Interfaces.Repositories.MenuRepo;
using Coddinggurrus.Core.Interfaces.Repositories.RoleMenuPermissions;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.ProblemsFaced;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Coddinggurrus.Core.Interfaces.Repositories.User;
using Coddinggurrus.Core.Interfaces.Services.Menus;
using Coddinggurrus.Core.Interfaces.Services.RoleMenuPermissions;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.ProblemsFaced;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Coddinggurrus.Core.Interfaces.Services.User;
using Coddinggurrus.Infrastructure.Repositories.MenuRepo;
using Coddinggurrus.Infrastructure.Repositories.RoleMenuPermissions;
using Coddinggurrus.Infrastructure.Repositories.Tutorials;
using Coddinggurrus.Infrastructure.Repositories.Tutorials.ProblemsFaced;
using Coddinggurrus.Infrastructure.Repositories.Tutorials.Web;
using Coddinggurrus.Infrastructure.Repositories.User;
using Microsoft.Extensions.Caching.Memory;

namespace Coddinggurrus.Api.Extensions
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfigurationRoot config)
        {
            return services
                .AddSingleton<IMemoryCache, MemoryCache>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserProfileService, UserProfileService>()
                .AddScoped<ICourseService, CourseService>()
                .AddScoped<IRoleMenuPermissionService, RoleMenuPermissionService>()
                .AddScoped<IMenuService, MenuService>()
                .AddScoped<ITopicService, TopicService>()
                .AddScoped<IContentService, ContentService>()
                .AddScoped<IWebContentService, WebContentService>()
                .AddScoped<IWidgetsService, WidgetsService>()
                .AddScoped<ICourseContentRepository, CourseContentRepository>()
                .AddScoped<IProblemTopicRepository, ProblemTopicRepository>()
                .AddScoped<IProblemContentRepository, ProblemContentRepository>()

                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUserProfileRepository, UserProfileRepository>()
                .AddScoped<ICourseRepository, CourseRepository>()
                .AddScoped<IMenuRepository, MenuRepository>()
                .AddScoped<IRoleMenuPermissionRepositry, RoleMenuPermissionRepositry>()
                .AddScoped<ITopicRepository, TopicRepository>()
                .AddScoped<IContentRepository, ContentRepository>()
                .AddScoped<IWebContentRepository, WebContentRepository>()
                .AddScoped<IWidgetsRepository, WidgetsRepository>()
                .AddScoped<ICourseContentService, CourseContentService>()
                .AddScoped<IProblemContentService, ProblemContentService>()
                .AddScoped<IProblemTopicService, ProblemTopicService>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
