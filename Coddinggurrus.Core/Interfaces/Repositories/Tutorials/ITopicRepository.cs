
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Models.Generic;

namespace Coddinggurrus.Core.Interfaces.Repositories.Tutorials
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> GetTopics(ListingParameter listingParameter);
        Task<int> AddTopic(Topic course);
        Task<bool> TitleExists(string title);
        Task<bool> UpdateTopic(Topic model);
        Task<bool> DeleteTopic(long Id);
        Task<Topic> GetTopicById(long id);
        Task<IEnumerable<DropdownListItems>> GetTopicsByCourseId(long courseId);
    }
}
