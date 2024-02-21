
using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Helper;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCourses(ListingParameter listingParameter);
        Task<int> AddCourse(Course course);
        Task<bool> TitleExists(string title);
        Task<bool> UpdateCourse(Course model);
        Task<bool> DeleteCourse(long Id);
        Task<Course> GetCourseById(long id);
    }
}
