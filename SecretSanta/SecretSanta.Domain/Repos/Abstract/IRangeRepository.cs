using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Repos.Abstract
{
    public interface IRangeRepository<T> : IRepository<T> where T : class
    {
        Task Add(IEnumerable<T> items);
        Task Delete(IEnumerable<T> items);
        Task Truncate();
    }
}
