using AutoMapper;
using Coddinggurrus.Api.Models.Admin.Tutorials;
using Coddinggurrus.Core.Entities.Tutorials;

namespace Coddinggurrus.Api.Mappings.Tutorials
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<TopicModel, Topic>();
        }
    }
}
