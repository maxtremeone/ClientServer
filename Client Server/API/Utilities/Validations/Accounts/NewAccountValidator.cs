using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class NewAccountValidator : AbstractValidator<NewAccountDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IAccountRepository _accountRepository;
        public NewAccountValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(a => a.Guid)
                .NotEmpty();

            RuleFor(a => a.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[0-9])(?=.*[A-Z]).{8,}$").WithMessage("Password invalid! Passwords must have at least 1 upper case and 1 number");

            RuleFor(a => a.Otp)
                .NotEmpty();

            RuleFor(a => a.IsUsed)
                .NotEmpty();

            RuleFor(a => a.ExpiredTime)
                .NotEmpty();
        }
    }
}
