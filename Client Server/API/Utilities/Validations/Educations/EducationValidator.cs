using API.Contracts;
using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations
{
    public class EducationValidator : AbstractValidator<EducationDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IEducationRepository _educationRepository;
        public EducationValidator(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;

            RuleFor(e => e.Guid)
                .NotEmpty();

            RuleFor(e => e.Major)
                .NotEmpty();

            RuleFor(e => e.Degree)
                .NotEmpty();

            RuleFor(e => e.Gpa)
                .NotEmpty();

            RuleFor(e => e.UniversityGuid)
                .NotEmpty();
        }
    }
}
