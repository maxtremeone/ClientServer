using API.Contracts;
using API.DTOs.Roles;
using API.Models;

namespace API.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<RoleDto> GetAll()
        {
            var roles = _roleRepository.GetAll();
            if (!roles.Any())
            {
                return Enumerable.Empty<RoleDto>(); // Role is null or not found;
            }

            var roleDtos = new List<RoleDto>();
            foreach (var role in roles)
            {
                roleDtos.Add((RoleDto)role);
            }

            return roleDtos; // role is found;
        }

        public RoleDto? GetByGuid(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return null; // role is null or not found;
            }

            return (RoleDto)role; // role is found;
        }

        public RoleDto? Create(NewRoleDto newRoleDto)
        {
            var role = _roleRepository.Create(newRoleDto);
            if (role is null)
            {
                return null; // role is null or not found;
            }

            return (RoleDto)role; // role is found;
        }

        public int Update(RoleDto roleDto)
        {
            var role = _roleRepository.GetByGuid(roleDto.Guid);
            if (role is null)
            {
                return -1; // role is null or not found;
            }

            Role toUpdate = roleDto;
            toUpdate.CreatedDate = role.CreatedDate;
            var result = _roleRepository.Update(toUpdate);

            return result ? 1 // role is updated;
                : 0; // role failed to update;
        }

        public int Delete(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid);
            if (role is null)
            {
                return -1; // role is null or not found;
            }

            var result = _roleRepository.Delete(role);

            return result ? 1 // role is deleted;
                : 0; // role failed to delete;
        }
    }
}
