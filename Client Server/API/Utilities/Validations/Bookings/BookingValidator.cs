using API.Contracts;
using API.DTOs.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Bookings
{
    public class BookingValidator : AbstractValidator<BookingDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingValidator(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;

            RuleFor(b => b.StartDate)
                .NotEmpty();

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
