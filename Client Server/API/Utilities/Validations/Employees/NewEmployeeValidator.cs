﻿using API.Contracts;
using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees
{
    public class NewEmployeeValidator :AbstractValidator<NewEmployeeDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IEmployeeRepository _employeeRepository;
        public NewEmployeeValidator(IEmployeeRepository employeeRepository) 
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
                .Must(IsDuplicateValue).WithMessage("Email already exist");

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(20)
                .Matches(@"^\+[0-9]").WithMessage("Phone number must start with +")
                .Must(IsDuplicateValue).WithMessage("Phone number already exist");
        }

        private bool IsDuplicateValue(string arg)
        {
            return _employeeRepository.IsNotExist(arg);
        }
    }
}
