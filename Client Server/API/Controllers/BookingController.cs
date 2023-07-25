using API.Services;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Bookings;
using API.DTOs.Roles;
using API.Utilities.Handlers;
using System.Net;
using API.DTOs.Rooms;

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
                return NotFound(new ResponseHandler<IEnumerable<BookingDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<BookingDto>>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<BookingDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewBookingDto newBookingDto)
        {
            var result = _bookingService.Create(newBookingDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewBookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<NewBookingDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Post Data",
                Data = newBookingDto
            });
        }

        [HttpPut]
        public IActionResult Update(BookingDto bookingDto)
        {
            var result = _bookingService.Update(bookingDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<BookingDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Succes",
                Data = bookingDto
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _bookingService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }
            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<BookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<BookingDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Success"
            });
        }
    }
}
