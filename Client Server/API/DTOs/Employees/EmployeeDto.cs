using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class EmployeeDto
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Birth_Date { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime Hiring_Date { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }

        public static implicit operator Employee(EmployeeDto employeeDto)
        {
            return new Employee
            {
                Guid = employeeDto.Guid,
                Nik = employeeDto.Nik,
                FirstName = employeeDto.First_Name,
                LastName = employeeDto.Last_Name,
                BirthDate = employeeDto.Birth_Date,
                Gender = employeeDto.Gender,
                HiringDate = employeeDto.Hiring_Date,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.Phone_Number,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator EmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                First_Name = employee.FirstName,
                Last_Name = employee.LastName,
                Birth_Date = employee.BirthDate,
                Gender = employee.Gender,
                Hiring_Date = employee.HiringDate,
                Email = employee.Email,
                Phone_Number = employee.PhoneNumber
            };
        }
    }
}
