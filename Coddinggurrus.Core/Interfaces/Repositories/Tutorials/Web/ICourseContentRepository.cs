
using Coddinggurrus.Core.Entities.Tutorials;

namespace Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web
{
    public interface ICourseContentRepository
    {
        Task<Topic?> GetTopicContentById(long topicId);
        Task<IEnumerable<Course>?> GetTopicsByCourseId(long courseId);
    }
}
