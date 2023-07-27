using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts
{
    public class RegisterValidator : AbstractValidator<RegisterDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        public RegisterValidator(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
        {
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;

            RuleFor(e => e.FirstName)
                .NotEmpty();

            RuleFor(e => e.LastName)
                .NotEmpty();

            RuleFor(e => e.BirthDate) 
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10)); 

            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();

            RuleFor(e => e.HiringDate)
                .NotEmpty();

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .Must(IsDuplicateValue).WithMessage("Email already exist");

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(@"^\+[0-9]").WithMessage("Phone number must start with +")
                .Must(IsDuplicateValue).WithMessage("Phone number already exist");

            RuleFor(e => e.Major)
                .NotEmpty();

            RuleFor(e => e.Degree)
                .NotEmpty();

            RuleFor(e => e.Gpa) 
                .NotEmpty();

            RuleFor(u => u.UnivCode) 
                .NotEmpty();

            RuleFor(u => u.UnivName) 
                .NotEmpty();

            RuleFor(a => a.Otp)
                .NotEmpty();

            RuleFor(a => a.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[0-9])(?=.*[A-Z]).{8,}$").WithMessage("Password invalid! Passwords must have at least 1 upper case and 1 number and 8 Digits");

            RuleFor(a => a.ConfirmPassword)
                .NotEmpty()
                .Equal(a => a.Password).WithMessage("Confirm Password Invalid!");

        }
        private bool IsDuplicateValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }
    }
}
