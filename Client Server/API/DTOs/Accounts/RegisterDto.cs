using API.Utilities.Enums;

namespace API.DTOs.Accounts
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public double Gpa { get; set; }
        public string UnivCode { get; set; }
        public string UnivName { get; set; }
        public int Otp { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
