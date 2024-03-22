using Coddinggurrus.Core.Dto.Tutorials;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials.Web
{
    public interface IWebCourseService
    {
        Task<IEnumerable<BrowseCourseDto>> GetBrowseTopics();
        Task<IEnumerable<CourseDto>> GetCoursesForSlider();
    }
}
