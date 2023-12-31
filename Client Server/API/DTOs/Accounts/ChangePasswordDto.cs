﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Accounts
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public int OTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}