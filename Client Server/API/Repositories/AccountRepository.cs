using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    public AccountRepository(BookingDbContext context) : base(context) { }

    //public Account? GetByEmployeeGuid(Guid employeeGuid)
    //{
    //    return _context.Accounts.FirstOrDefault(a => a.Guid == employeeGuid);
    //} ini guid comment aja cukup di service, karena malah jadi conflict
}