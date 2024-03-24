using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Tutorials;
using Coddinggurrus.Core.Dto.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;

namespace Coddinggurrus.Api.Mappings.Tutorials
{
    public class ContentProfile: Profile
    {
        public ContentProfile()
        {
            CreateMap<ContentModel, Content>();
            CreateMap<Content, ContentDto>();
        }
    }
}
