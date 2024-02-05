
using Coddinggurrus.Core.Models.Course;

namespace Coddinggurrus.Core.Interfaces.Repositories.Course
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseModel>> GetCourses(int skip, int take, string searchText = "");
        Task<int> AddCourse(CourseModel course);
        Task<bool> TitleExists(string title);
        Task<bool> UpdateCourse(CourseModel model);
        Task<bool> DeleteCourse(long Id);
    }
}
