using API.Services;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Roles;
using API.DTOs.Universities;
using API.Utilities.Handlers;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<RoleDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<RoleDto>>()
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
            var result = _roleService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<RoleDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewRoleDto newRoleDto)
        {

            var result = _roleService.Create(newRoleDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<NewRoleDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Post Data",
                Data = newRoleDto
            });
        }

        [HttpPut]
        public IActionResult Update(RoleDto roleDto)
        {
            var result = _roleService.Update(roleDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<RoleDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Succes",
                Data = roleDto
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _roleService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }
            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<RoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<RoleDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Success"
            });
        }
    }
}
