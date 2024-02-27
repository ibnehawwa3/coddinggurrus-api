using AutoMapper;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Repositories.Tutorials;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials
{
    public class ContentService : BaseService, IContentService
    {
        private readonly IContentRepository _contentRepository;
        public ContentService(IContentRepository contentRepository, IConfiguration config, IMapper mapper) : base(config, mapper)
        {
            _contentRepository = contentRepository;
        }

        public async Task<int> AddContent(Content content)
        {
            return await _contentRepository.AddContent(content);
        }

        public async Task<bool> DeleteContent(long Id)
        {
            return await _contentRepository.DeleteContent(Id);
        }

        public async Task<Content> GetContentById(long id)
        {
            return await _contentRepository.GetContentById(id);
        }

        public async Task<IEnumerable<Content>> GetContents(ListingParameter listingParameter)
        {
            return await _contentRepository.GetContents(listingParameter);
        }

        public async Task<bool> TitleExists(string title)
        {
            var exists = await _contentRepository.TitleExists(title);
            return exists;
        }

        public async Task<bool> UpdateContent(Content model)
        {
            return await _contentRepository.UpdateContent(model);
        }
    }
}