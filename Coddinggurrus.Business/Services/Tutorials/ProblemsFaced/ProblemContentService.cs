using AutoMapper;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.ProblemsFaced;
using Coddinggurrus.Core.Interfaces.Services.Tutorials.ProblemsFaced;
using Coddinggurrus.Core.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials.ProblemsFaced
{
    public class ProblemContentService : BaseService, IProblemContentService
    {
        private readonly IProblemContentRepository _problemContentRepository;
        public ProblemContentService(IProblemContentRepository problemContentRepository, IConfiguration config, IMapper mapper, IMemoryCache cache) : base(config, mapper, cache)
        {
            _problemContentRepository = problemContentRepository;
        }

        public async Task<int> AddContent(Content content)
        {
            return await _problemContentRepository.AddContent(content);
        }

        public async Task<bool> DeleteContent(long Id)
        {
            return await _problemContentRepository.DeleteContent(Id);
        }

        public async Task<ContentViewModel> GetContentById(long id)
        {
            return await _problemContentRepository.GetContentById(id);
        }

        public async Task<IEnumerable<Content>> GetContents(ListingParameter listingParameter)
        {
            return await _problemContentRepository.GetContents(listingParameter);
        }

        public async Task<bool> TitleExists(string title, long topicId)
        {
            var exists = await _problemContentRepository.TitleExists(title, topicId);
            return exists;
        }

        public async Task<bool> UpdateContent(Content model)
        {
            return await _problemContentRepository.UpdateContent(model);
        }
    }
}
