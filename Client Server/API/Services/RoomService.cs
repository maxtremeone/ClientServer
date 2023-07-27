using API.Contracts;
using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.Models;
using System.Threading.Tasks.Dataflow;

namespace API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RoomService(IRoomRepository roomRepository, IBookingRepository bookingRepository, IEmployeeRepository employeeRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
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

        public IEnumerable<BookedRoomDto> GetAllBookedRoom()
        {
            var today = DateTime.Today;
            var getBookedRoomToday = (from b in _bookingRepository.GetAll()
                                      join e in _employeeRepository.GetAll() on b.EmployeeGuid equals e.Guid
                                      join r in _roomRepository.GetAll() on b.RoomGuid equals r.Guid
                                      where b.StartDate.Date == today.Date
                                      select new BookedRoomDto // Assuming BookedRoomDto is the correct type
                                      {
                                          BookingGuid = b.Guid,
                                          RoomName = r.Name,
                                          Status = b.Status,
                                          Floor = r.Floor,
                                          BookedBy = e.FirstName + " " + e.LastName
                                      }).ToList();

            if (!getBookedRoomToday.Any())
            {
                return null;
            }

            return getBookedRoomToday;
        }

    }
}
