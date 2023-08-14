using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            //RuleFor(e => e.Nik)
            //    .NotEmpty()
            //    .MaximumLength(6);

            RuleFor(e => e.FirstName)
                .NotEmpty();

            RuleFor(e => e.LastName)
                .NotEmpty();

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

            RuleFor(e => e.Gender)
                //.NotEmpty();
                .NotNull()
                .IsInEnum();

            RuleFor(e => e.HiringDate)
                .NotEmpty();

            RuleFor(e => e.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Email is not valid")
                 .Must((e, email) => CheckValidity(e.Guid, email) is true).WithMessage("Phone Number already exists");

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(@"^\+[0-9]").WithMessage("Phone number must start with +")
                .Must((e, phone) => CheckValidity(e.Guid, phone) is true).WithMessage("Phone Number already exists");
        }

        private bool IsDuplicateValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }

        private bool CheckValidity(Guid guid, string value)
        {
            bool valid = false;
            (string email, string phone) = GetByGuid(guid);
            if (email == value || phone == value)
            {
                valid = true;
            }
            return IsDuplicateValue(value) || valid;
        }

        private (string?, string?) GetByGuid(Guid guid)
        {
            Employee employee = _employeeRepository.GetByGuid(guid);
            return (employee.Email, employee.PhoneNumber);
        }
    }
}
