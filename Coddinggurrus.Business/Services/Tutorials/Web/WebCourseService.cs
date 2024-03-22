using AutoMapper;
using Coddinggurrus.Core.Dto.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials.Web
{
    public class WebCourseService : BaseService, IWebCourseService
    {
        private readonly IWebCourseRepository _webCourseRepository;
        public WebCourseService(IWebCourseRepository webCourseRepository, IConfiguration config, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _webCourseRepository = webCourseRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<CourseDto>> GetBrowseTopics()
        {
            if (!Cache.TryGetValue("BrowseTopics", out IEnumerable<Course>? courses))
            {
                courses = await _webCourseRepository.GetBrowseTopics();
                if (courses.Any())
                {
                    Cache.Set("BrowseTopics", courses, TimeSpan.FromMinutes(60));
                }
            }
            return Mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseDto>> GetCoursesForSlider()
        {
            if (!Cache.TryGetValue("CoursesForSlider", out IEnumerable<Course>? courses))
            {
                courses = await _webCourseRepository.GetCoursesForSlider();
                if (courses.Any())
                {
                    Cache.Set("CoursesForSlider", courses, TimeSpan.FromMinutes(60));
                }
            }
            return Mapper.Map<IEnumerable<CourseDto>>(courses);
        }

    }
}
