using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly BookingDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, 
        IEducationRepository educationRepository, IUniversityRepository universityRepository, BookingDbContext dbContext)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _dbContext = dbContext;
        }

        public IEnumerable<AccountDto> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return Enumerable.Empty<AccountDto>(); // account is null or not found;
            }

            var accountDtos = new List<AccountDto>();
            foreach (var account in accounts)
            {
                accountDtos.Add((AccountDto)account);
            }

            return accountDtos; // account is found;
        }

        public AccountDto? GetByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return null; // account is null or not found;
            }

            return (AccountDto)account; // account is found;
        }

        public AccountDto? Create(NewAccountDto newAccountDto)
        {
            var account = _accountRepository.Create(newAccountDto);
            if (account is null)
            {
                return null; // account is null or not found;
            }

            return (AccountDto)account; // account is found;
        }

        public int Update(AccountDto accountDto)
        {
            var Account = _accountRepository.GetByGuid(accountDto.Guid);
            if (Account is null)
            {
                return -1; // account is null or not found;
            }

            Account toUpdate = accountDto;
            toUpdate.CreatedDate = Account.CreatedDate;
            var result = _accountRepository.Update(toUpdate);

            return result ? 1 // account is updated;
                : 0; // account failed to update;
        }

        public int Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return -1; // account is null or not found;
            }

            var result = _accountRepository.Delete(account);

            return result ? 1 // account is deleted;
                : 0; // account failed to delete;
        }

        public int Login(LoginDto loginDto)
        {
            var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);

            if (getEmployee is null)
            {
                return 0; // Employee not found
            }

            var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);

            if (getAccount.Password == loginDto.Password)
            {
                return 1; // Login success
            }

            return 0;
        }
        public int Register(RegisterDto registerDto)
        {
            // ini untuk cek emaik sama phone number udah ada atau belum
            if (!_employeeRepository.IsNotExist(registerDto.Email) || !_employeeRepository.IsNotExist(registerDto.PhoneNumber))
            {
                return 0; // kalau sudah ada, pendaftaran gagal.
            }

            var newNik = GenerateHandler.Nik(_employeeRepository.GetLastNik()); //karena niknya generate, sebelumnya kalo ga dikasih ini niknya null jadi error
            var employeeGuid = Guid.NewGuid(); // Generate GUID baru untuk employee

            // Buat objek Employee dengan nilai GUID baru
            var employee = new Employee
            {
                Guid = employeeGuid, //ambil dari variabel yang udah dibuat diatas
                Nik = newNik, //ini juga
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                HiringDate = registerDto.HiringDate,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber
            };
            _dbContext.Employees.Add(employee);

           
            var education = new Education
            {
                Guid = employeeGuid, // Gunakan employeeGuid
                Major = registerDto.Major,
                Degree = registerDto.Degree,
                Gpa = (float)registerDto.Gpa
            };
            _dbContext.Educations.Add(education);

            // Cek apakah kode univ sudah ada di tabel
            var existingUniversity = _universityRepository.GetByCode(registerDto.UnivCode);
            if (existingUniversity is null)
            {
                // Jika universitas belum ada, buat objek University baru dan simpan
                var university = new University
                {
                    Code = registerDto.UnivCode,
                    Name = registerDto.UnivName
                };
                _dbContext.Universities.Add(university);

                // Set nilai UniversityGuid pada objek Education menggunakan GUID baru dari universitas yang baru dibuat
                education.UniversityGuid = university.Guid;
            }
            else
            {
                // kalau universitas sudah ada, gunakan UniversityGuid yang sudah ada untuk objek Education
                education.UniversityGuid = existingUniversity.Guid;
            }

            
            var account = new Account
            {
                Guid = employeeGuid, // Gunakan employeeGuid
                Otp = registerDto.Otp,//sementara ini dicoba gabisa diisi angka nol didepan, tadi masukin 098 error
                Password = registerDto.Password
            };
            _dbContext.Accounts.Add(account);

            try
            {
                _dbContext.SaveChanges(); // Simpan perubahan
                return 1; // Pendaftaran berhasil
            }
            catch (Exception)
            {
                // ini kalo ada kesalahan menyimpan ke tabel, di catch pake exception
                return -1; // Pendaftaran gagal
            }
        }

        public int ForgotPassword(ForgotPasswordOTPDto forgotPassword)
        {
            var employee = _employeeRepository.GetByEmail(forgotPassword.Email);
            if (employee is null)
                return 0; // Email not found

            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account is null)
                return -1;
            var otp = new Random().Next(111111, 999999);
            var isUpdated = _accountRepository.Update(new Account
            {
                Guid = account.Guid,
                Password = account.Password,
                ExpiredTime = DateTime.Now.AddMinutes(5),
                Otp = otp,
                IsUsed = false,
                CreatedDate = account.CreatedDate,
                ModifiedDate = DateTime.Now
            });

            if (!isUpdated)
                return -1;

            forgotPassword.Email = $"{otp}";
            return 1;
        }

        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var isExist = _employeeRepository.CheckEmail(changePasswordDto.Email);
            if (isExist is null)
            {
                return -1; //Account not found
            }

            var getAccount = _accountRepository.GetByGuid(isExist.Guid);
            if (getAccount.Otp != changePasswordDto.OTP)
            {
                return 0;
            }

            if (getAccount.IsUsed == true)
            {
                return 1;
            }

            if (getAccount.ExpiredTime < DateTime.Now)
            {
                return 2;
            }

            var account = new Account
            {
                Guid = getAccount.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount.CreatedDate,
                Otp = getAccount.Otp,
                ExpiredTime = getAccount.ExpiredTime,
                Password = changePasswordDto.NewPassword
            };

            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return 0; //Account Not Update
            }

            return 3;
        }
    }
}
