using API.Services;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Educations;

namespace API.Controllers
{
    [ApiController]
    [Route("api/educations")]
    public class EducationController : ControllerBase
    {
        private readonly EducationService _educationService;

        public EducationController(EducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _educationService.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _educationService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(NewEducationDto newEducationDto)
        {
            var result = _educationService.Create(newEducationDto);
            if (result is null)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(EducationDto educationDto)
        {
            var result = _educationService.Update(educationDto);

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
            var result = _educationService.Delete(guid);
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
