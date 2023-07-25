using API.Services;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Employees;
using API.DTOs.Roles;
using API.Utilities.Handlers;
using System.Net;
using API.DTOs.Rooms;

namespace API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<EmployeeDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<EmployeeDto>>()
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
            var result = _employeeService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<EmployeeDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewEmployeeDto newEmployeeDto)
        {
            var result = _employeeService.Create(newEmployeeDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<NewEmployeeDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Post Data",
                Data = newEmployeeDto
            });
        }

        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            var result = _employeeService.Update(employeeDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<EmployeeDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Succes",
                Data = employeeDto
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _employeeService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }
            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<EmployeeDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Success"
            });
        }
    }
}
