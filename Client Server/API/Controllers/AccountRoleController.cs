using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/accountroles")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountroleRepository;

        public AccountRoleController(IAccountRoleRepository accountroleRepository)
        {
            _accountroleRepository = accountroleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountroleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountroleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(AccountRole accountrole)
        {
            var result = _accountroleRepository.Create(accountrole);
            if (result is null)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountRole accountrole)
        {
            var check = _accountroleRepository.GetByGuid(accountrole.Guid);
            if (check is null)
            {
                return NotFound("Guid is not found");
            }

            var result = _accountroleRepository.Update(accountrole);
            if (!result)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Update success");
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _accountroleRepository.GetByGuid(guid);
            if (data is null)
            {
                return NotFound("Guid is not found");
            }

            var result = _accountroleRepository.Delete(data);
            if (!result)
            {
                return StatusCode(500, "Error Retrieve from database");
            }

            return Ok("Delete success");
        }
    }
}
