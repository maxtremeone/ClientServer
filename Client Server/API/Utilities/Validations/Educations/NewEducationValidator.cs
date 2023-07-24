using API.Contracts;
using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations
{
    public class NewEducationValidator :AbstractValidator<NewEducationDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IEducationRepository _educationRepository;
        public NewEducationValidator(IEducationRepository educationRepository) 
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
