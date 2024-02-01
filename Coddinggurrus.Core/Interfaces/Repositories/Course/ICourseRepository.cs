
using Coddinggurrus.Core.Models.Course;

namespace Coddinggurrus.Core.Interfaces.Repositories.Course
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseModel>> GetCourses(int skip, int take, string searchText = "");
    }
}
