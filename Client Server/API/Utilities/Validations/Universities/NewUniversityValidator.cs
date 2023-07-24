using API.Contracts;
using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.Universities
{
    public class NewUniversityValidator : AbstractValidator<NewUniversityDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IUniversityRepository _universityRepository;
        public NewUniversityValidator(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;

            RuleFor(u => u.Code)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
