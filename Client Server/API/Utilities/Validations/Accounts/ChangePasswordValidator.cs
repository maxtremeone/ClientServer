using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        private readonly IAccountRepository _accountRepository;

        public ChangePasswordValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(a => a.Email)
               .NotEmpty();

            RuleFor(a => a.NewPassword)
                .NotEmpty()
                .Matches(@"^(?=.*[0-9])(?=.*[A-Z]).{8,}$").WithMessage("Password invalid! Passwords must have at least 1 upper case and 1 number");

            RuleFor(a => a.ConfirmPassword)
                .NotEmpty()
                .Equal(a => a.NewPassword);

            RuleFor(a => a.OTP)
                .NotEmpty();
        }
    }
}
