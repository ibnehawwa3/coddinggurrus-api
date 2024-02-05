using Coddinggurrus.Core.Interfaces.Repositories.Course;
using Coddinggurrus.Core.Interfaces.Services.Course;
using Coddinggurrus.Core.Models.Course;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Course
{
    public class CourseService : BaseService, ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository,IConfiguration config) : base(config)
        {
            _courseRepository= courseRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseModel>> GetCourses(int pageNo, int pageSize, string searchText = "")
        {
            var skip = (pageNo * pageSize) - pageSize;
            return await _courseRepository.GetCourses(skip, pageSize, searchText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<int> AddCourse(CourseModel course)
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
        public async Task<bool> UpdateCourse(CourseModel model)
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
