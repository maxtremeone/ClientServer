using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        bool IsNotExist(string value);
        //string? GetlastNik();
        string? GetLastNik();
        Employee? GetByEmail(string email);

   
        Employee? GetByGuid(Guid guid);
        // Tambahkan metode Create untuk Employee
        Employee Create(Employee employee);
    }

}