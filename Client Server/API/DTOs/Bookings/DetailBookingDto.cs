using API.Utilities.Enums;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.DTOs.Bookings
{
    public class DetailBookingDto
    {
        //Employee
        public Guid BookingGuid { get; set; }
        public string BookedNik { get; set; }
        public string BookedBy { get; set;}

        //Room
        public string RoomName { get; set;}

        //Book
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel StatusLevel { get; set; }
        public string Remarks { get; set; }
       


    }
}
