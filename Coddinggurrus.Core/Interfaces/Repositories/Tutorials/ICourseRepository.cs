
using Coddinggurrus.Core.Entities;

namespace Coddinggurrus.Core.Interfaces.Repositories.Tutorials
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCourses(int skip, int take, string searchText = "");
        Task<int> AddCourse(Course course);
        Task<bool> TitleExists(string title);
        Task<bool> UpdateCourse(Course model);
        Task<bool> DeleteCourse(long Id);
    }
}
