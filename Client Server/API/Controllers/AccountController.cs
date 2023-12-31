﻿using API.Services;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Accounts;
using API.DTOs.Roles;
using API.Utilities.Handlers;
using System.Net;
using API.DTOs.Rooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    //[Authorize]
    [EnableCors]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<IEnumerable<AccountDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<AccountDto>>()
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
            var result = _accountService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<AccountDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success retrieve data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewAccountDto newAccountDto)
        {
            var result = _accountService.Create(newAccountDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<NewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<NewAccountDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Post Data",
                Data = newAccountDto
            });
        }

        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            var result = _accountService.Update(accountDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<AccountDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Succes",
                Data = accountDto
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _accountService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }
            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Error Retrieve from database"
                });
            }

            return Ok(new ResponseHandler<AccountDto>()
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Success"
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _accountService.Login(loginDto);

            if (result is "-1")
            {
                return NotFound(new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email or Password is incorrect"
                });
            }

            if (result is "-2")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error when generate token"
                });
            }

            return Ok(new ResponseHandler<object>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success",
                Data = new TokenDto
                {
                    Token = result
                }
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            int result = _accountService.Register(registerDto);

            if (result == 0)
            {
                // Jika pendaftaran gagal, kirim respons dengan status 400 Bad Request
                return BadRequest(new ResponseHandler<RegisterDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email or PhoneNumber is already registered."
                });
            }
            else if (result == 1)
            {
                // Jika pendaftaran berhasil, kirim respons dengan status 200 OK
                return Ok(new ResponseHandler<RegisterDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Registration Success."
                });
            }
            else
            {
                // Jika terjadi kesalahan lain, kirim respons dengan status 500 Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<RegisterDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieve from database."
                });
            }
        }
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgetPassword(ForgotPasswordOTPDto forgotPasswordDto)
        {
            var isUpdated = _accountService.ForgotPasswordOTPDto(forgotPasswordDto);
            if (isUpdated is 0)
                return NotFound(new ResponseHandler<ForgotPasswordOTPDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not found"
                });

            if (isUpdated is -1)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ForgotPasswordOTPDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieving data from the database"
                });

            return Ok(new ResponseHandler<ForgotPasswordOTPDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Otp has been sent to your email"
            });
        }

        [AllowAnonymous]
        [HttpPut("ChangePassword")]
        public IActionResult UpdatePassword(ChangePasswordDto changePasswordDto)
        {
            var update = _accountService.ChangePassword(changePasswordDto);
            if (update is -1)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email not found"
                });
            }

            if (update is -2)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "OTP doesn't match"
                });
            }

            if (update is -3)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "OTP Is Used"
                });
            }

            if (update is -4)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Otp Already Expired"
                });
            }

            return Ok(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Password Updated"
            });
        }
    }
}