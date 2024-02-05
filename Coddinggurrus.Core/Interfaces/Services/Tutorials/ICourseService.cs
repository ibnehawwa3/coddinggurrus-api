
using Coddinggurrus.Core.Entities;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCourses(int pageNo, int pageSize, string searchText = "");
        Task<int> AddCourse(Course course);
        Task<bool> TitleExists(string title);
        Task<bool> UpdateCourse(Course model);
        Task<bool> DeleteCourse(long Id);

    }
}
