using API.Contracts;
using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles
{
    public class NewRoleValidator : AbstractValidator<NewRoleDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IRoleRepository _roleRepository;
        public NewRoleValidator(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

            RuleFor(r => r.Name)
                .NotEmpty();
        }
    }
}
