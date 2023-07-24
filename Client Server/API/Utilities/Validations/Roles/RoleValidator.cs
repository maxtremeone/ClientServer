using API.Contracts;
using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class RoleValidator : AbstractValidator<RoleDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IRoleRepository _roleRepository;
        public RoleValidator(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

            RuleFor(r => r.Name)
                .NotEmpty();
        }
    }
}
