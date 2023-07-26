using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EducationRepository : GeneralRepository<Education>, IEducationRepository
{
    public EducationRepository(BookingDbContext context) : base(context) { }

    //public Education? GetByEmployeeGuid(Guid employeeGuid)
    //{
    //    return _context.Educations.FirstOrDefault(e => e.Guid == employeeGuid);
    //}ini guid comment aja cukup di service, karena malah jadi conflict
}