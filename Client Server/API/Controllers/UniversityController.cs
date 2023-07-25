using API.DTOs.Universities;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.Utilities.Handlers;
using System.Collections;

namespace API.Controllers
{
    [ApiController]
    [Route("api/univerities")]
    public class UniversityController : ControllerBase
    {
        private readonly UniversityService _universityService; //IUniversityRepository _universityRepository diganti jadi UniversityService _universityService

        public UniversityController(UniversityService universityService) //(IUniversityRepository universityRepository) diganti jadi (UniversityService universityService)
        {
            _universityService = universityService;// _universityRepository = universityRepository ganti jadi  _universityService = universityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universityService.GetAll();//_universityRepository ganti  jadi _universityService
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<UniversityDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<UniversityDto>>()
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
            var result = _universityService.GetByGuid(guid);//_universityRepository ganti jadi _universityService
            if (result is null)
            {
                return NotFound(new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<UniversityDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewUniversityDto newUniversityDto)//University university ganti jadi (NewUniversityDto newUniversityDto)
        {
            var result = _universityService.Create(newUniversityDto);//_universityRepository.Create(university); ganti jadi _universityService.Create(newUniversityDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewUniversityDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<NewUniversityDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Post Data",
                Data = newUniversityDto
            });
        }

        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var result = _universityService.Update(universityDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<UniversityDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Succes",
                Data = universityDto
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _universityService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }
            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UniversityDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<UniversityDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Success"
            });
        }
    }
}
