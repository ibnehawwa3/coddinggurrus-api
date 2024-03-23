
namespace Coddinggurrus.Core.Dto.Tutorials
{
    public record struct CourseTopicDto(long Id,
                     string Title,
                     IEnumerable<TopicDto>? Topics);
}
