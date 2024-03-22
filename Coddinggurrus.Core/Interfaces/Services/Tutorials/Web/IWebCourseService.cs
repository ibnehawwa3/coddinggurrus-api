using Coddinggurrus.Core.Dto.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials.Web
{
    public interface IWebCourseService
    {
        Task<IEnumerable<CourseDto>> GetCoursesForSlider();
    }
}
