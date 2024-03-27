
using Coddinggurrus.Core.Dto.Tutorials;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials.Web
{
    public interface ICourseContentService
    {
        Task<TopicContentDto> GetTopicContentById(long topicId);
        Task<IEnumerable<CourseTopicDto>> GetTopicsByCourseId(long courseId);
    }
}
