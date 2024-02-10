using Coddinggurrus.Core.Entities;

namespace Coddinggurrus.Core.Interfaces.Repositories
{
    public interface IRepository
    {
        Task Add<T>(object o) where T : Entity;
        Task Add<T>(IEnumerable<object> o) where T : IEnumerable<Entity>;
        Task Update<T>(object o) where T : Entity;
        Task Delete<T>(object o) where T : Entity;
    }
}
