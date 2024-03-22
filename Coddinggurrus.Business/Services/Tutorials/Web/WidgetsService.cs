using AutoMapper;
using Coddinggurrus.Core.Dto.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.Web;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials.Web
{
    public class WidgetsService : BaseService, IWidgetsService
    {
        private readonly IWidgetsRepository _widgetsRepository;
        public WidgetsService(IWidgetsRepository widgetsRepository, IConfiguration config, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _widgetsRepository = widgetsRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<BrowseCourseDto>> GetBrowseTopics()
        {
            if (!Cache.TryGetValue("BrowseTopics", out IEnumerable<Course>? courses))
            {
                courses = await _widgetsRepository.GetBrowseTopics();
                if (courses.Any())
                {
                    Cache.Set("BrowseTopics", courses, TimeSpan.FromMinutes(60));
                }
            }
            return Mapper.Map<IEnumerable<BrowseCourseDto>>(courses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseDto>> GetCoursesForSlider()
        {
            if (!Cache.TryGetValue("CoursesForSlider", out IEnumerable<Course>? courses))
            {
                courses = await _widgetsRepository.GetCoursesForSlider();
                if (courses.Any())
                {
                    Cache.Set("CoursesForSlider", courses, TimeSpan.FromMinutes(60));
                }
            }
            return Mapper.Map<IEnumerable<CourseDto>>(courses);
        }

    }
}
