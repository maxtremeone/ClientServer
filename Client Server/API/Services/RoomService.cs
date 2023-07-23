using API.Contracts;
using API.DTOs.Rooms;
using API.Models;

namespace API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IEnumerable<RoomDto> GetAll()
        {
            var rooms = _roomRepository.GetAll();
            if (!rooms.Any())
            {
                return Enumerable.Empty<RoomDto>(); // Room is null or not found;
            }

            var roomDtos = new List<RoomDto>();
            foreach (var room in rooms)
            {
                roomDtos.Add((RoomDto)room);
            }

            return roomDtos; // room is found;
        }

        public RoomDto? GetByGuid(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return null; // room is null or not found;
            }

            return (RoomDto)room; // room is found;
        }

        public RoomDto? Create(NewRoomDto newRoomDto)
        {
            var room = _roomRepository.Create(newRoomDto);
            if (room is null)
            {
                return null; // room is null or not found;
            }

            return (RoomDto)room; // room is found;
        }

        public int Update(RoomDto roomDto)
        {
            var room = _roomRepository.GetByGuid(roomDto.Guid);
            if (room is null)
            {
                return -1; // room is null or not found;
            }

            Room toUpdate = roomDto;
            toUpdate.CreatedDate = room.CreatedDate;
            var result = _roomRepository.Update(toUpdate);

            return result ? 1 // room is updated;
                : 0; // room failed to update;
        }

        public int Delete(Guid guid)
        {
            var room = _roomRepository.GetByGuid(guid);
            if (room is null)
            {
                return -1; // room is null or not found;
            }

            var result = _roomRepository.Delete(room);

            return result ? 1 // room is deleted;
                : 0; // room failed to delete;
        }
    }
}
