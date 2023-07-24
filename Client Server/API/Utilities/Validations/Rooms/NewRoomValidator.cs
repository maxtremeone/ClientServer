using API.Contracts;
using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms
{
    public class NewRoomValidator : AbstractValidator<NewRoomDto> //diisi DTO karena semua request masuk DTO
    {
        private readonly IRoomRepository _roomRepository;
        public NewRoomValidator(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;

            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(r => r.Floor)
                .NotEmpty();

            RuleFor(r => r.Capacity)
                .NotEmpty();
        }
    }
}
