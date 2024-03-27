
namespace Coddinggurrus.Core.Dto.Tutorials
{
    public record struct TopicContentDto(long Id,
                    string Title,
                    ContentDto? Content);
}
