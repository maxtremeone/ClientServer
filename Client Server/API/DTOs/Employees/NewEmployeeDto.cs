using API.Models;
using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employees
{
    public class NewEmployeeDto
    {
        //public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        //[EmailAddress] //Data Annotation untuk validasi, tetpi ini menambah tanggung jawab gasesuai sama SRP, harusnya pake fluent validation jadi validasinya ga dilakukan di DTO, install librarynya dulu
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public static implicit operator Employee(NewEmployeeDto newEmployeeDto)
        {
            return new Employee
            {
                Guid = new Guid(),
                //Nik = newEmployeeDto.Nik,
                FirstName = newEmployeeDto.FirstName,
                LastName = newEmployeeDto.LastName,
                BirthDate = newEmployeeDto.BirthDate,
                Gender = newEmployeeDto.Gender,
                HiringDate = newEmployeeDto.HiringDate,
                Email = newEmployeeDto.Email,
                PhoneNumber = newEmployeeDto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator NewEmployeeDto(Employee employee)
        {
            return new NewEmployeeDto
            {
                //Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber
            };
        }
    }
}
