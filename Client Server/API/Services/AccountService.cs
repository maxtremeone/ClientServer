using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TokenHandler = Microsoft.IdentityModel.Tokens.TokenHandler;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;
        private readonly BookingDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, 
        IEducationRepository educationRepository, IUniversityRepository universityRepository, 
        BookingDbContext dbContext, IEmailHandler emailHandler, ITokenHandler tokenHandler, IAccountRoleRepository accountRoleRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _accountRoleRepository = accountRoleRepository;
            _dbContext = dbContext;
            _tokenHandler = tokenHandler;
            _emailHandler = emailHandler;

        }

        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            //var isExist = _employeeRepository.CheckEmail(changePasswordDto.Email);
            //if (isExist is null)
            //{
            //    return -1; //Account not found
            //}

            //var getAccount = _accountRepository.GetByGuid(isExist.Guid);
            //if (getAccount.Otp != changePasswordDto.OTP)
            //{
            //    return 0;
            //}

            var getAccountDetail = (from e in _employeeRepository.GetAll()
                                    join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                    where e.Email == changePasswordDto.Email
                                    select a).FirstOrDefault();
            _accountRepository.Clear();

            if (getAccountDetail is null)
            {
                return -1; // Account not found
            }

            if (getAccountDetail.Otp != changePasswordDto.OTP)
            {
                return -2;
            }

            if (getAccountDetail.IsUsed)
            {
                return -3;
            }

            if (getAccountDetail.ExpiredTime < DateTime.Now)
            {
                return -4;
            }

            var account = new Account
            {
                Guid = getAccountDetail.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccountDetail.CreatedDate,
                Otp = getAccountDetail.Otp,
                ExpiredTime = getAccountDetail.ExpiredTime,
                Password = HashingHandler.GenerateHash(changePasswordDto.NewPassword)
            };

            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return -5; //Account Not Update
            }

            return 1;
        }
        public int ForgotPasswordOTPDto(ForgotPasswordOTPDto forgotPasswordOTPDto)
        {
            var getAccountDetail = (from e in _employeeRepository.GetAll()
                                    join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                    where e.Email == forgotPasswordOTPDto.Email
                                    select a).FirstOrDefault();
            _accountRepository.Clear();

            if (getAccountDetail is null)
            {
                return 0;
            }

            var otp = new Random().Next(111111, 999999);
            var account = new Account
            {
                Guid = getAccountDetail.Guid,
                Password = getAccountDetail.Password,
                ExpiredTime = DateTime.Now.AddMinutes(5),
                Otp = otp,
                IsUsed = false,
                CreatedDate = getAccountDetail.CreatedDate,
                ModifiedDate = DateTime.Now
            };

            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return -1;
            }

            _emailHandler.SendEmail(forgotPasswordOTPDto.Email, "Booking - Forgot Password", $"Your Otp is {otp}");

            return 1;
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

            return result
                ? 1  // account is updated;
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

            return result
                ? 1  // account is deleted;
                : 0; // account failed to delete;
        }

        public string Login(LoginDto loginDto)
        {
            var employeeAccount = from e in _employeeRepository.GetAll()
                                  join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                  where e.Email == loginDto.Email && HashingHandler.ValidateHash(loginDto.Password, a.Password)
                                  select new LoginDto()
                                  {
                                      Email = e.Email,
                                      Password = a.Password
                                  };

            if (!employeeAccount.Any())
            {
                return "-1"; // Email or Password incorrect.    
            }

            var employee = _employeeRepository.GetByEmail(loginDto.Email);
            var getRoles = _accountRoleRepository.GetRoleNamesByAccountGuid(employee.Guid);

            var claims = new List<Claim>
            {
                new Claim("Guid", employee.Guid.ToString()),
                new Claim("FullName", $"{employee.FirstName} {employee.LastName}"),
                new Claim("Email", employee.Email)
            };

            foreach (var role in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var generatedToken = _tokenHandler.GenerateToken(claims);
            if (generatedToken is null) 
            {
                return "-2";
            }

            return generatedToken;
                                
        }

        public int Register(RegisterDto registerDto)
        {
            // ini untuk cek emaik sama phone number udah ada atau belum
            if (!_employeeRepository.IsNotExist(registerDto.Email) ||
                !_employeeRepository.IsNotExist(registerDto.PhoneNumber))
            {
                return 0; // kalau sudah ada, pendaftaran gagal.
            }

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var university = _universityRepository.GetByCode(registerDto.UniversityCode);
                if (university is null)
                {
                    // Jika universitas belum ada, buat objek University baru dan simpan
                    var createUniversity = _universityRepository.Create(new University
                    {
                        Code = registerDto.UniversityCode,
                        Name = registerDto.UniversityName
                    });

                    university = createUniversity;
                }

                var newNik =
                    GenerateHandler.Nik(_employeeRepository.GetLastNik()); //karena niknya generate, sebelumnya kalo ga dikasih ini niknya null jadi error
                var employeeGuid = Guid.NewGuid(); // Generate GUID baru untuk employee

                // Buat objek Employee dengan nilai GUID baru
                var employee = _employeeRepository.Create(new Employee
                {
                    Guid = employeeGuid, //ambil dari variabel yang udah dibuat diatas
                    Nik = newNik,        //ini juga
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    BirthDate = registerDto.BirthDate,
                    Gender = registerDto.Gender,
                    HiringDate = registerDto.HiringDate,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber
                });


                var education = _educationRepository.Create(new Education
                {
                    Guid = employeeGuid, // Gunakan employeeGuid
                    Major = registerDto.Major,
                    Degree = registerDto.Degree,
                    Gpa = registerDto.Gpa,
                    UniversityGuid = university.Guid
                });

                var account = _accountRepository.Create(new Account
                {
                    Guid = employeeGuid, // Gunakan employeeGuid
                    Otp = 1,             //sementara ini dicoba gabisa diisi angka nol didepan, tadi masukin 098 error
                    IsUsed = true,
                    Password = HashingHandler.GenerateHash(registerDto.Password),
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

                var accountRole = _accountRoleRepository.Create(new NewAccountRoleDto
                {
                    AccountGuid = account.Guid,
                    RoleGuid = Guid.Parse("d1141f82-b41b-4f74-9ad5-08db91bf7e71")
                });

                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return -1;
            }
        }
    }
}
