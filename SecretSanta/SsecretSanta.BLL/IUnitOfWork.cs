using SecretSanta.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.BLL
{
    public interface IUnitOfWork
    {
        Task<User> AddUser(User user);

        Task<IEnumerable<SecretSantaPair>> GetSecretSantas();
    }
}
