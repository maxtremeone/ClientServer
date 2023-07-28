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
           .NotEmpty()
           .EmailAddress();

            RuleFor(e => e.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[0-9])(?=.*[A-Z]).{8,}$").WithMessage("Password invalid! Passwords must have at least 1 upper case and 1 number");
        }
    }
}
