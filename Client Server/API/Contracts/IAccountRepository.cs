using API.Models;

namespace API.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        //Account? GetByEmployeeGuid(Guid employeeGuid);
        //// Tambahkan metode Create untuk Account
        Account Create(Account account);
    }

}