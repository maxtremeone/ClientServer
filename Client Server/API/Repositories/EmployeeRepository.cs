using API.Contracts;
using API.Data;
using API.DTOs.Employees;
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

    //public bool IsNotExist(string value)
    //{
    //    return _context.Set<Employee>()
    //                   .SingleOrDefault(e => e.Email == value || e.PhoneNumber == value) == null;
    //}

    //public string? GetLastNik()
    //{
    //    return _context.Set<Employee>().ToList().LastOrDefault()?.Nik;
    //}

    public string? GetLastNik()
    {
        return _context.Set<Employee>().ToList().OrderBy(emp => emp.Nik).LastOrDefault()?.Nik;
    }

    public Employee? GetByEmail(string email)
    {
        return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
    }

    public Employee? CheckEmail(string email)
    {
        return _context.Set<Employee>().FirstOrDefault(u => u.Email == email);
    }

    public Guid GetLastEmployeeGuid()
    {
        return _context.Set<Employee>().ToList().LastOrDefault().Guid;
    }

}