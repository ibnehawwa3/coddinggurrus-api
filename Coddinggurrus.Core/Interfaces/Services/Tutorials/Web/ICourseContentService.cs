
using Coddinggurrus.Core.Dto.Tutorials;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials.Web
{
    public interface ICourseContentService
    {
        Task<IEnumerable<CourseTopicDto>> GetTopicsByCourseId(long courseId);
    }
}
