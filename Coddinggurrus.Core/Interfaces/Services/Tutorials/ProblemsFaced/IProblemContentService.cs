using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.ViewModels;

namespace Coddinggurrus.Core.Interfaces.Services.Tutorials.ProblemsFaced
{
    public interface IProblemContentService
    {
        Task<IEnumerable<Content>> GetContents(ListingParameter listingParameter);
        Task<int> AddContent(Content content);
        Task<bool> TitleExists(string title, long topicId);
        Task<bool> UpdateContent(Content model);
        Task<bool> DeleteContent(long Id);
        Task<ContentViewModel> GetContentById(long id);
    }
}
