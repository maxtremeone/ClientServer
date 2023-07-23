using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Accounts
{
    public class NewAccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static implicit operator Account(NewAccountDto newAccountDto)
        {
            return new Account
            {
                Guid = newAccountDto.Guid,
                Password = newAccountDto.Password,
                Otp = newAccountDto.Otp,
                IsUsed = newAccountDto.IsUsed,
                ExpiredTime = newAccountDto.ExpiredTime,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewAccountDto(Account account)
        {
            return new NewAccountDto
            {
                Guid = account.Guid,
                Password = account.Password,
                Otp = account.Otp,
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime
            };
        }
    }
}
