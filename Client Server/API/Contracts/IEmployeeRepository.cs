using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        bool IsNotExist(string value);
        //string? GetlastNik();
        string? GetLastNik();
    }

}