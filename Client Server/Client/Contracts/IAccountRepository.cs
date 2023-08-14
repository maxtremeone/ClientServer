using API.DTOs.Accounts;
using API.Models;
using API.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        public Task<ResponseHandler<TokenDto>> Login(LoginDto entity);
    }
}