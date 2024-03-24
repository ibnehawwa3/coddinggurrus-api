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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public async Task<TopicContentDto> GetTopicContentById(long topicId)
        {
            string cacheKey = $"Topic-Content-By-Id-{topicId}";
            if (!Cache.TryGetValue(cacheKey, out Topic? topicContent))
            {
                topicContent = await _courseContentRepository.GetTopicContentById(topicId);
                if (topicContent is not null)
                {
                    Cache.Set(cacheKey, topicContent, TimeSpan.FromMinutes(60));
                }
            }
            return Mapper.Map<TopicContentDto>(topicContent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
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
