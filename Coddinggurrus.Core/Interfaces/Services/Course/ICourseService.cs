using Coddinggurrus.Core.Models.Course;

namespace Coddinggurrus.Core.Interfaces.Services.Course
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetCourses(int pageNo, int pageSize, string searchText = "");
        Task<int> AddCourse(CourseModel course);
        Task<bool> TitleExists(string title);
        Task<bool> UpdateCourse(CourseModel model);
        Task<bool> DeleteCourse(long Id);

    }
}
