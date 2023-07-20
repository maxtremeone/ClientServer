using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookingDbContext _context;

        public EmployeeRepository(BookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>()
                           .ToList();
        }

        public Employee? GetByGuid(Guid guid)
        {
            var data = _context.Set<Employee>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public Employee? Create(Employee employee)
        {
            try
            {
                _context.Set<Employee>()
                        .Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Employee employee)
        {
            try
            {
                _context.Entry(employee)
                        .State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Employee employee)
        {
            try
            {
                _context.Set<Employee>()
                        .Remove(employee);
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
