using Coddinggurrus.Core.Interfaces.Repositories.Course;
using Coddinggurrus.Core.Interfaces.Services.Course;
using Coddinggurrus.Core.Models.Course;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Business.Services.Course
{
    public class CourseService : BaseService, ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository,IConfiguration config) : base(config)
        {
            _courseRepository= courseRepository;
        }
        public async Task<IEnumerable<CourseModel>> GetCourses(int pageNo, int pageSize, string searchText = "")
        {
            var skip = (pageNo * pageSize) - pageSize;
            return await _courseRepository.GetCourses(skip, pageSize, searchText);
        }

        public async Task<int> AddCourse(CourseModel course)
        {
            return await _courseRepository.AddCourse(course);
        }

        public async Task<bool> TitleExists(string title)
        {
            var exists = await _courseRepository.TitleExists(title);
            return exists;
        }
    }
}
