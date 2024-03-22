
using Coddinggurrus.Core.Entities.Tutorials;

namespace Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web
{
    public interface IWidgetsRepository
    {
        Task<IEnumerable<Course>?> GetBrowseTopics();
        Task<IEnumerable<Course>> GetCoursesForSlider();
    }
}
