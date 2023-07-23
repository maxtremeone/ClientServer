using API.DTOs.Universities;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound("Not data found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _universityService.GetByGuid(guid);//_universityRepository ganti jadi _universityService
            if (result is null)
            {
                return NotFound("Guid is not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(NewUniversityDto newUniversityDto)//University university ganti jadi (NewUniversityDto newUniversityDto)
        {
            var result = _universityService.Create(newUniversityDto);//_universityRepository.Create(university); ganti jadi _universityService.Create(newUniversityDto);
            if (result is null)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var result = _universityService.Update(universityDto);

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
            var result = _universityService.Delete(guid);

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
