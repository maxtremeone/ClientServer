using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly BookingDbContext _context;

        public EducationRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Education> GetAll()
        {
            return _context.Set<Education>()
                           .ToList();
        }

        public Education? GetByGuid(Guid guid)
        {
            var data = _context.Set<Education>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public Education? Create(Education education)
        {
            try
            {
                _context.Set<Education>()
                        .Add(education);
                _context.SaveChanges();
                return education;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Education education)
        {
            try
            {
                _context.Entry(education)
                        .State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Education education)
        {
            try
            {
                _context.Set<Education>()
                        .Remove(education);
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
