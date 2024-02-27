using AutoMapper;
using Coddinggurrus.Core.Entities.Tutorials;
using Coddinggurrus.Core.Helper;
using Coddinggurrus.Core.Interfaces.Services.Tutorials;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Business.Services.Tutorials
{
    public class ContentService : BaseService, IContentService
    {
        public ContentService(IConfiguration config, IMapper mapper) : base(config, mapper)
        {
        }

        public Task<int> AddContent(Content content)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContent(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<Topic> GetContentById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Content>> GetContents(ListingParameter listingParameter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TitleExists(string title)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateContent(Content model)
        {
            throw new NotImplementedException();
        }
    }
}