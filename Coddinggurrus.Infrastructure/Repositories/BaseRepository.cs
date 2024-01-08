using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected IConfiguration Config { get; private set; }
        protected readonly string CoddingGurrusDbConnectionString;

        public BaseRepository(IConfiguration config)
        {
            Config = config;

            // Use GetSection and Value to get the value of the configuration key
            CoddingGurrusDbConnectionString = config.GetSection("ConnectionStrings")["CoddingGurrusDb"];
        }
    }
}
