using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Repos.Concrete
{
    public class SecretSantaPairRepository : IRangeRepository<SecretSantaPair>
    {
        private readonly DbContext _context;
        private readonly DbSet<SecretSantaPair> _repo;

        public SecretSantaPairRepository(DbContext context)
        {
            _context = context;
            _repo = context.Set<SecretSantaPair>();
        }


        async Task IRangeRepository<SecretSantaPair>.Add(IEnumerable<SecretSantaPair> items)
        {
            await _repo.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        async Task<SecretSantaPair> IRepository<SecretSantaPair>.Add(SecretSantaPair item)
        {
            var addedPair = (await _repo.AddAsync(item)).Entity;
            await _context.SaveChangesAsync();
            return addedPair;
        }

        async Task IRangeRepository<SecretSantaPair>.Delete(IEnumerable<SecretSantaPair> items)
        {
            var toDelete = await _repo.Where(e => items.Select(i => i.Id).Contains(e.Id)).ToArrayAsync();

            if (toDelete.Count() == 0)
                throw new IndexOutOfRangeException("No such elements.");

            _repo.RemoveRange(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task Truncate()
        {
            string cmd = $"TRUNCATE TABLE {_context.Model.FindEntityType(typeof(SecretSantaPair)).Relational().TableName}";
            await _context.Database.ExecuteSqlCommandAsync(cmd);
        }

        async Task IRepository<SecretSantaPair>.Delete(int id)
        {
            var toDelte = await _repo.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (toDelte == null)
                throw new IndexOutOfRangeException("No cush element.");
            _repo.Remove(toDelte);
            await _context.SaveChangesAsync();
        }

        async Task<IEnumerable<SecretSantaPair>> IRepository<SecretSantaPair>.Get()
        {
            // simple lazy loading protection
            var pairs = await _repo.Select(i => new SecretSantaPair
            {
                Id = i.Id,
                GiverId = i.GiverId,
                Giver = new User { Id = i.Giver.Id, FirstName = i.Giver.FirstName, LastName = i.Giver.LastName },
                ReceiverId = i.ReceiverId,
                Receiver = new User { Id = i.Receiver.Id, FirstName = i.Receiver.FirstName, LastName = i.Receiver.LastName }
            })
                .ToArrayAsync();
            return pairs;
        }

        async Task<SecretSantaPair> IRepository<SecretSantaPair>.Get(int id)
        {
            var item = await _repo.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (item == null)
                throw new IndexOutOfRangeException("No such item");
            return item;
        }

        async Task IRepository<SecretSantaPair>.Update(SecretSantaPair item)
        {
            _repo.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
