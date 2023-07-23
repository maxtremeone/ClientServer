using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                Guid = accountDto.Guid,
                Password = accountDto.Password,
                Otp = accountDto.Otp,
                IsUsed = accountDto.IsUsed,
                ExpiredTime = accountDto.ExpiredTime,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
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
