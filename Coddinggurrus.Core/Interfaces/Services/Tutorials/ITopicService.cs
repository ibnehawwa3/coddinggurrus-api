using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>> GetTopics(ListingParameter listingParameter);
        Task<int> AddTopic(Topic topic);
        Task<bool> TitleExists(string title);
        Task<bool> UpdateTopic(Topic model);
        Task<bool> DeleteTopic(long Id);
        Task<Topic> GetTopicById(long id);
    }
}
