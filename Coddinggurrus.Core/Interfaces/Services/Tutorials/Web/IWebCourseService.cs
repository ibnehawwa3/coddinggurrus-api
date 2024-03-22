using Coddinggurrus.Core.Dto.Tutorials;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials.Web
{
    public interface IWebCourseService
    {
        Task<IEnumerable<CourseDto>> GetBrowseTopics();
        Task<IEnumerable<CourseDto>> GetCoursesForSlider();
    }
}
