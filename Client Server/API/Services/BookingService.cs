using API.Contracts;
using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
using ClientServer.DTOs.Bookings;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
        }

        public IEnumerable<BookingDto> GetAll()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return Enumerable.Empty<BookingDto>(); // Booking is null or not found;
            }

            var bookingDtos = new List<BookingDto>();
            foreach (var booking in bookings)
            {
                bookingDtos.Add((BookingDto)booking);
            }

            return bookingDtos; // booking is found;
        }

        public BookingDto? GetByGuid(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return null; // booking is null or not found;
            }

            return (BookingDto)booking; // booking is found;
        }

        public BookingDto? Create(NewBookingDto newBookingDto)
        {
            var booking = _bookingRepository.Create(newBookingDto);
            if (booking is null)
            {
                return null; // booking is null or not found;
            }

            return (BookingDto)booking; // booking is found;
        }

        public int Update(BookingDto bookingDto)
        {
            var booking = _bookingRepository.GetByGuid(bookingDto.Guid);
            if (booking is null)
            {
                return -1; // booking is null or not found;
            }

            Booking toUpdate = bookingDto;
            toUpdate.CreatedDate = booking.CreatedDate;
            var result = _bookingRepository.Update(toUpdate);

            return result ? 1 // booking is updated;
                : 0; // booking failed to update;
        }

        public int Delete(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return -1; // booking is null or not found;
            }

            var result = _bookingRepository.Delete(booking);

            return result ? 1 // booking is deleted;
                : 0; // booking failed to delete;
        }

        public IEnumerable<DetailBookingDto> GetAllDetailBooking()
        {
            var getBookingsDetail = (from b in _bookingRepository.GetAll()
                                     join e in _employeeRepository.GetAll() on b.EmployeeGuid equals e.Guid
                                     join r in _roomRepository.GetAll() on b.RoomGuid equals r.Guid
                                     select new DetailBookingDto
                                     {
                                         BookingGuid = b.Guid,
                                         BookedNik = e.Nik,
                                         BookedBy = e.FirstName + " " + e.LastName,
                                         RoomName = r.Name,
                                         StartDate = b.StartDate,
                                         EndDate = b.EndDate,
                                         StatusLevel = b.Status,
                                         Remarks = b.Remarks
                                     });
            if (!getBookingsDetail.Any() || getBookingsDetail is null)
            {
                return Enumerable.Empty<DetailBookingDto>();
            }

            return getBookingsDetail; // booking is found;
        }


        public DetailBookingDto? GetDetailBookingByGuid(Guid guid)
        {
            var allBookings = GetAllDetailBooking();
            return allBookings.FirstOrDefault(b => b.BookingGuid == guid);
        }


        public IEnumerable<RoomDto> FreeRoomToday()
        {
            List<RoomDto> roomDtos = new List<RoomDto>();
            var bookings = GetAll();
            var freeBookings = bookings.Where(b => b.Status == StatusLevel.Done);
            var freeBookingsToday = freeBookings.Where(b => b.EndDate < DateTime.Now);
            foreach (var booking in freeBookingsToday)
            {
                var roomGuid = booking.RoomGuid;
                var room = _roomRepository.GetByGuid(roomGuid);
                RoomDto roomDto = new RoomDto()
                {
                    Guid = roomGuid,
                    Capacity = room.Capacity,
                    Floor = room.Floor,
                    Name = room.Name
                };
                roomDtos.Add(roomDto);
            }

            if (!roomDtos.Any())
            {
                return null; // No free room today
            }

            return roomDtos; // free room today
        }

        public IEnumerable<BookingLengthDto> BookingLength()
        {
            List<BookingLengthDto> listBookingLength = new List<BookingLengthDto>();
            TimeSpan workingHour = new TimeSpan(8, 30, 0);
            var timeSpan = new TimeSpan();
            var bookings = GetAll();
            foreach (var booking in bookings)
            {
                var currentDate = booking.StartDate;
                var endDate = booking.EndDate;

                while (currentDate <= endDate)
                {
                    // Memeriksa apakah hari saat ini adalah Sabtu atau Minggu
                    if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        // Hari kerja, menghitung waktu kerja dengan memperhitungkan jam
                        DateTime openRoom = currentDate.Date.AddHours(9); // Misalnya, waktu kerja dimulai pada pukul 09:00
                        DateTime closeRoom = currentDate.Date.AddHours(17).AddMinutes(30); // Misalnya, waktu kerja selesai pada pukul 17:30

                        TimeSpan dayTime = closeRoom - openRoom;
                        timeSpan += dayTime;
                    }

                    currentDate = currentDate.AddDays(1); // Pindah ke hari berikutnya
                }

                var room = _roomRepository.GetByGuid(booking.RoomGuid);

                var bookingLengthDto = new BookingLengthDto()
                {
                    RoomGuid = booking.RoomGuid,
                    RoomName = room.Name,
                    BookingLength = timeSpan.TotalHours
                };
                listBookingLength.Add(bookingLengthDto);
            }

            if (!listBookingLength.Any())
            {
                return null;
            }

            return listBookingLength;
        }
    }
}
