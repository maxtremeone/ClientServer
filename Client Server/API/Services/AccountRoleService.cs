using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;

namespace API.Services
{
    public class AccountRoleService
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        public IEnumerable<AccountRoleDto> GetAll()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            if (!accountRoles.Any())
            {
                return Enumerable.Empty<AccountRoleDto>(); // AccountRole is null or not found;
            }

            var accountRoleDtos = new List<AccountRoleDto>();
            foreach (var accountRole in accountRoles)
            {
                accountRoleDtos.Add((AccountRoleDto)accountRole);
            }

            return accountRoleDtos; // AccountRole is found;
        }

        public AccountRoleDto? GetByGuid(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null)
            {
                return null; // accountRole is null or not found;
            }

            return (AccountRoleDto)accountRole; // accountRole is found;
        }

        public AccountRoleDto? Create(NewAccountRoleDto newAccountRoleDto)
        {
            var accountRole = _accountRoleRepository.Create(newAccountRoleDto);
            if (accountRole is null)
            {
                return null; // accountRole is null or not found;
            }

            return (AccountRoleDto)accountRole; // accountRole is found;
        }

        public int Update(AccountRoleDto accountRoleDto)
        {
            var accountRole = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
            if (accountRole is null)
            {
                return -1; // accountRole is null or not found;
            }

            AccountRole toUpdate = accountRoleDto;
            toUpdate.CreatedDate = accountRole.CreatedDate;
            var result = _accountRoleRepository.Update(toUpdate);

            return result ? 1 // accountRole is updated;
                : 0; // accountRole failed to update;
        }

        public int Delete(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null)
            {
                return -1; // accountRole is null or not found;
            }

            var result = _accountRoleRepository.Delete(accountRole);

            return result ? 1 // accountRole is deleted;
                : 0; // accountRole failed to delete;
        }
    }
}
