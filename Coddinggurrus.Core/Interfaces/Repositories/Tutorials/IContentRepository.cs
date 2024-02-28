using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;

namespace Coddinggurrus.Core.Interfaces.Repositories.Tutorials
{
    public interface IContentRepository
    {
        Task<IEnumerable<Content>> GetContents(ListingParameter listingParameter);
        Task<int> AddContent(Content content);
        Task<bool> TitleExists(string title, long topicId);
        Task<bool> UpdateContent(Content model);
        Task<bool> DeleteContent(long Id);
        Task<Content> GetContentById(long id);
    }
}
