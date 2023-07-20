using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BookingDbContext _context;

        public RoleRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Set<Role>()
                           .ToList();
        }

        public Role? GetByGuid(Guid guid)
        {
            var data = _context.Set<Role>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public Role? Create(Role role)
        {
            try
            {
                _context.Set<Role>()
                        .Add(role);
                _context.SaveChanges();
                return role;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Role role)
        {
            try
            {
                _context.Entry(role)
                        .State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Role role)
        {
            try
            {
                _context.Set<Role>()
                        .Remove(role);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
