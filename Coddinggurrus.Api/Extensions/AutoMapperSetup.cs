using AutoMapper;
using Coddinggurrus.Api.Mappings;

namespace Coddinggurrus.Api.Extensions
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            return services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CourseProfile());
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new MenuProfile());
            }).CreateMapper());
        }
    }
}
