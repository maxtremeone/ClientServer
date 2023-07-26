using API.Models;

namespace API.Contracts
{
    public interface IEducationRepository : IGeneralRepository<Education>
    {
        // Tambahkan metode Create untuk Education
        Education Create(Education education);
    }

}