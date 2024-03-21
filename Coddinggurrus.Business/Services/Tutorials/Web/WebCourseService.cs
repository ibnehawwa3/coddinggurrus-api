using AutoMapper;
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
        public async Task<IEnumerable<Course>> GetCoursesForSlider()
        {
            return await _webCourseRepository.GetCoursesForSlider();
        }
    }
}
