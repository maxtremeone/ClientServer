using API.Services;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Bookings;

namespace API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingService.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(NewBookingDto newBookingDto)
        {
            var result = _bookingService.Create(newBookingDto);
            if (result is null)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(BookingDto bookingDto)
        {
            var result = _bookingService.Update(bookingDto);

            if (result is -1)
            {
                return NotFound("Guid is not found");
            }

            if (result is 0)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Update success");
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _bookingService.Delete(guid);
            if (result is -1)
            {
                return NotFound("Guid is not found");
            }

            if (result is 0)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Delete success");
        }
    }
}
