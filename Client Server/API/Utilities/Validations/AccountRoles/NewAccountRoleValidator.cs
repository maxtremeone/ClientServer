using API.Contracts;
using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles
{
    public class NewAccountRoleValidator : AbstractValidator<NewAccountRoleDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        public NewAccountRoleValidator(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;

            RuleFor(a => a.AccountGuid)
                .NotEmpty();

            RuleFor(a => a.RoleGuid)
                .NotEmpty();
        }
    }
}
