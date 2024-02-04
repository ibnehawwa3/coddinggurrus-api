using Coddinggurrus.Core.Entities;
using Coddinggurrus.Core.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace Coddinggurrus.Infrastructure.Repositories
{
    public class BaseRepository : IRepository
    {
        protected IConfiguration Config { get; private set; }
        protected readonly string CoddingGurrusDbConnectionString;

        public BaseRepository(IConfiguration config)
        {
            Config = config;

            // Use GetSection and Value to get the value of the configuration key
            CoddingGurrusDbConnectionString = config.GetSection("ConnectionStrings")["CoddingGurrusDb"];
        }

        public Task Add<T>(object o) where T : Entity
        {
            throw new NotImplementedException();
        }

        public Task Add<T>(IEnumerable<object> o) where T : IEnumerable<Entity>
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(object o) where T : Entity
        {
            throw new NotImplementedException();
        }

        public Task Delete<T>(object o) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}
