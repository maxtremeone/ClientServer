using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly BookingDbContext _context;

        public AccountRoleRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>()
                           .ToList();
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            var data = _context.Set<AccountRole>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public AccountRole? Create(AccountRole accountrole)
        {
            try
            {
                _context.Set<AccountRole>()
                        .Add(accountrole);
                _context.SaveChanges();
                return accountrole;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(AccountRole accountrole)
        {
            try
            {
                _context.Entry(accountrole)
                        .State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(AccountRole accountrole)
        {
            try
            {
                _context.Set<AccountRole>()
                        .Remove(accountrole);
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
