using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(BookingDbContext context) : base(context) { }

    public bool IsNotExist(string value)
    {
        return _context.Set<Employee>()
                       .SingleOrDefault(e => e.Email.Contains(value)
                       || e.PhoneNumber.Contains(value)) is null;
    }

    public string? GetLastNik()
    {
        return _context.Set<Employee>().ToList().LastOrDefault()?.Nik;
    }
}