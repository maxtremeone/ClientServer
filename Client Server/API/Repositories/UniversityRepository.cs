using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly BookingDbContext _context;

        public UniversityRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>()
                           .ToList();
        }

        public University? GetByGuid(Guid guid)
        {
            var data = _context.Set<University>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public University? Create(University university)
        {
            try
            {
                _context.Set<University>()
                        .Add(university);
                _context.SaveChanges();
                return university;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(University university)
        {
            try
            {
                _context.Entry(university)
                        .State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(University university)
        {
            try
            {
                _context.Set<University>()
                        .Remove(university);
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
