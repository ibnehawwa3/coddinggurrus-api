using Coddinggurrus.Core.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coddinggurrus.Core.Interfaces.Services.Course
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetCourses(int pageNo, int pageSize, string searchText = "");
        Task<int> AddCourse(CourseModel course);
    }
}
