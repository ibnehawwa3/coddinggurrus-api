using Coddinggurrus.Core.Interfaces.Repositories.Tutorials.Web;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories.Tutorials.Web
{
    public class WebContentRepository : BaseRepository, IWebContentRepository
    {
        public WebContentRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
        }
    }
}
