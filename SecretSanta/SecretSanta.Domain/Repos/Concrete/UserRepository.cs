using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Repos.Concrete
{
    public class UserRepository : IRepository<User>
    {
        private readonly DbContext _context;
        private readonly DbSet<User> _repo;

        public UserRepository(DbContext context)
        {
            _context = context;
            _repo = context.Set<User>();
        }


        public async Task<User> Add(User item)
        {
            var addedUser = (await _repo.AddAsync(item)).Entity;
            await _context.SaveChangesAsync();
            return addedUser;
        }

        public async Task Delete(int id)
        {
            var toDelte = await _repo.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (toDelte == null)
                throw new IndexOutOfRangeException("No such element.");
            _repo.Remove(toDelte);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> Get() => await _repo.ToArrayAsync();

        public async Task<User> Get(int id)
        {
            var item = await _repo.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (item == null)
                throw new IndexOutOfRangeException("No such item");
            return item;
        }

        public async Task Update(User item)
        {
            _repo.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
