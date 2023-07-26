using API.Models;

namespace API.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University> 
    {
        // Tambahkan metode Create untuk University
        University Create(University university);
        University? GetByCode(string code);
    }

}