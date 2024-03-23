using AutoMapper;
using Coddinggurrus.Core.Dto.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials.Web
{
    public class CourseContentService : BaseService, ICourseContentService
    {
        private readonly ICourseContentRepository _courseContentRepository;
        public CourseContentService(ICourseContentRepository courseContentRepository, IConfiguration config, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _courseContentRepository = courseContentRepository;
        }

        public async Task<IEnumerable<CourseTopicDto>> GetTopicsByCourseId(long courseId)
        {
            string cacheKey = $"Topics-By-Course-Id-{courseId}";
            if (!Cache.TryGetValue(cacheKey, out IEnumerable<Course>? courses))
            {
                courses = await _courseContentRepository.GetTopicsByCourseId(courseId);
                if (courses.Any())
                {
                    Cache.Set(cacheKey, courses, TimeSpan.FromMinutes(60));
                }
            }
            return Mapper.Map<IEnumerable<CourseTopicDto>>(courses);
        }
    }
}
