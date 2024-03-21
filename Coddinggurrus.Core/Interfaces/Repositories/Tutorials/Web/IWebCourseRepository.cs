
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;

namespace Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web
{
    public interface IWebCourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesForSlider();
    }
}
