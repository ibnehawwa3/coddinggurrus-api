using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials
{
    public class CourseService : BaseService, ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository, IConfiguration config) : base(config)
        {
            _courseRepository = courseRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Course>> GetCourses(int pageNo, int pageSize, string searchText = "")
        {
            var skip = (pageNo * pageSize) - pageSize;
            return await _courseRepository.GetCourses(skip, pageSize, searchText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<int> AddCourse(Course course)
        {
            return await _courseRepository.AddCourse(course);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<bool> TitleExists(string title)
        {
            var exists = await _courseRepository.TitleExists(title);
            return exists;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCourse(Course model)
        {
            return await _courseRepository.UpdateCourse(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCourse(long Id)
        {
            return await _courseRepository.DeleteCourse(Id);
        }
    }
}
