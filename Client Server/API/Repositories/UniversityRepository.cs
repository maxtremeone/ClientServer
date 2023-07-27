using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingDbContext context) : base(context) { }


        public University? GetByCode(string code)
        {
            return _context.Set<University>().SingleOrDefault(u => u.Code == code);
        }

        public Guid GetLastUniversityGuid()
        {
            return _context.Set<University>().ToList().LastOrDefault().Guid;
        }
    }
}
