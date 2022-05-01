using Microsoft.Extensions.Logging;
using SecretSanta.BLL.Services;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SecretSanta.BLL
{
    /// <summary>
    /// Unit of work without of DTOs
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IRepository<User> _users;
        private readonly IRangeRepository<SecretSantaPair> _santas;

        public UnitOfWork(IRepository<User> users, IRangeRepository<SecretSantaPair> santas)
        {
            _users = users;
            _santas = santas;
        }

        /// <summary>
        /// User addition logic
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> AddUser(User user)
        {
            using (var transaction = new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted },
                TransactionScopeAsyncFlowOption.Enabled))
            {
                var addedUser = await _users.Add(user);
                await RearangeSecretSantas();

                transaction.Complete();
                // simple lazy loading protection
                return new User { Id = addedUser.Id, FirstName = addedUser.FirstName, LastName = addedUser.LastName };
            }
        }

        /// <summary>
        /// Rearengment of secret santas list
        /// </summary>
        /// <returns></returns>
        private async Task RearangeSecretSantas()
        {
            // shuffle list of users
            var rng = new Random();
            var users = (await _users.Get()).OrderBy(i => rng.Next()).ToList();

            // make links between shuffled users
            var llinkedList = new LinkedList<User>(users);

            // make pairs
            var santaPairs = llinkedList
                .Nodes()
                .Select(i => new SecretSantaPair
                {
                    Giver = i.Value,
                    Receiver = i?.Next?.Value ?? llinkedList.First.Value // close the sequence
                })
                .ToList();

            // purge existing pairs table
            await _santas.Truncate();

            // write new pairs to the table
            await _santas.Add(santaPairs);
        }

        public async Task<IEnumerable<SecretSantaPair>> GetSecretSantas() => await _santas.Get();
    }
}
