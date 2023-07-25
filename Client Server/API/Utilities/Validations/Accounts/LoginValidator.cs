using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class LoginValidator : AbstractValidator<LoginDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IEmployeeRepository _employeeRepository;
        public LoginValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(e => e.Email)
                .NotEmpty();

            RuleFor(e => e.Password)
                .NotEmpty();
        }
    }
}
