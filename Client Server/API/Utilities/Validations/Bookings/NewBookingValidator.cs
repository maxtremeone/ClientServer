using API.Contracts;
using API.DTOs.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Bookings
{
    public class NewBookingValidator : AbstractValidator<NewBookingDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IBookingRepository _bookingRepository;
        public NewBookingValidator(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;

            RuleFor(b => b.StartDate)
                .NotEmpty();
                //.GreaterThanOrEqualTo(DateTime.Now.AddDays(1)).WithMessage("Booking Must 1 Day");

            RuleFor(b => b.EndDate)
               .NotEmpty();

            RuleFor(b => b.Status)
                .NotNull()
                .IsInEnum();

            RuleFor(b => b.Remarks)
                .NotEmpty();

            RuleFor(b => b.RoomGuid)
                .NotEmpty();

            RuleFor(b => b.EmployeeGuid)
                .NotEmpty();
        }
    }
}
