using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Set<Booking>()
                           .ToList();
        }

        public Booking? GetByGuid(Guid guid)
        {
            var data = _context.Set<Booking>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public Booking? Create(Booking booking)
        {
            try
            {
                _context.Set<Booking>()
                        .Add(booking);
                _context.SaveChanges();
                return booking;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Booking booking)
        {
            try
            {
                _context.Entry(booking)
                        .State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Booking booking)
        {
            try
            {
                _context.Set<Booking>()
                        .Remove(booking);
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
