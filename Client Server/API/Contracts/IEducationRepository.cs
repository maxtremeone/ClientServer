using API.Models;

namespace API.Contracts
{
    public interface IEducationRepository
    {
        IEnumerable<Education> GetAll();
        Education? GetByGuid(Guid guid);
        Education? Create(Education Education);
        bool Update(Education Education);
        bool Delete(Education Education);
    }
}
