using Coddinggurrus.Core.Dto.Tutorials;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials.Web
{
    public interface IWidgetsService
    {
        Task<IEnumerable<BrowseCourseDto>> GetBrowseTopics();
        Task<IEnumerable<CourseDto>> GetCoursesForSlider();
    }
}
