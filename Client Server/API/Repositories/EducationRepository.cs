using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EducationRepository : GeneralRepository<Education>, IEducationRepository
{
    public EducationRepository(BookingDbContext context) : base(context) { }
}