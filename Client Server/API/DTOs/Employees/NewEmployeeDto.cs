using API.Models;
using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employees
{
    public class NewEmployeeDto
    {
        //public string Nik { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Birth_Date { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime Hiring_Date { get; set; }
        //[EmailAddress] //Data Annotation untuk validasi, tetpi ini menambah tanggung jawab gasesuai sama SRP, harusnya pake fluent validation jadi validasinya ga dilakukan di DTO, install librarynya dulu
        public string Email { get; set; }
        public string Phone_Number { get; set; }

        public static implicit operator Employee(NewEmployeeDto newEmployeeDto)
        {
            return new Employee
            {
                Guid = new Guid(),
                //Nik = newEmployeeDto.Nik,
                FirstName = newEmployeeDto.First_Name,
                LastName = newEmployeeDto.Last_Name,
                BirthDate = newEmployeeDto.Birth_Date,
                Gender = newEmployeeDto.Gender,
                HiringDate = newEmployeeDto.Hiring_Date,
                Email = newEmployeeDto.Email,
                PhoneNumber = newEmployeeDto.Phone_Number,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewEmployeeDto(Employee employee)
        {
            return new NewEmployeeDto
            {
                //Nik = employee.Nik,
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
