using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Repos.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();

        Task<T> Get(int id);

        Task<T> Add(T item);

        Task Update(T item);

        Task Delete(int id);
    }
}
