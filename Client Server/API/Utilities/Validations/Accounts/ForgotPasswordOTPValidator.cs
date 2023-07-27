using API.Contracts;
using API.DTOs.Accounts;
using API.Repositories;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class ForgotPasswordOTPValidator : AbstractValidator<ForgotPasswordOTPDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IAccountRepository _accountRepository;
        public ForgotPasswordOTPValidator(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            RuleFor(e => e.Email)
               .NotEmpty()
               .EmailAddress().WithMessage("Email is not valid");
        }
    }
}
