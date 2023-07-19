using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class BookingDbContext : DbContext //konfigurasi untuk koneksi
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { } //Konstruktor

        //DbSet untuk mengApply Model supaya tabelnya dibuat diDtaabase
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) //unique constrain
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                        .HasIndex(e => new
                        {
                            e.Nik,
                            e.Email,
                            e.PhoneNumber
                        }).IsUnique();

            //Many Education with One University (N:1)
            modelBuilder.Entity<Education>() //Education memiliki Foreign Key
                        .HasOne(e => e.University)
                        .WithMany(u => u.Educations)
                        .HasForeignKey(e => e.UniversityGuid);


            //one University with many education (1:N)
            //modelBuilder.Entity<University>()
            //            .HasMany(u => u.Educations)
            //            .WithOne(e => e.University)
            //            .HasForeignKey(e => e.UniversityGuid); //Pake salah satu aja

            //one Education with One Employees
            modelBuilder.Entity<Employee>()
                        .HasOne(emp => emp.Education)
                        .WithOne(e => e.Employee)
                        .HasForeignKey<Education>(emp => emp.Guid);

            //modelBuilder.Entity<Education>()
            //    .HasOne(e => e.Employee)
            //    .WithOne(e => e.Education)
            //    .HasForeignKey<Education>(e => e.Guid);


            //Many Booking with one Employee
            modelBuilder.Entity<Booking>()
                        .HasOne(b => b.Employee)
                        .WithMany(emp => emp.Bookings)
                        .HasForeignKey(b => b.EmployeeGuid);
                     

            //Many Booking with one room
            modelBuilder.Entity<Booking>()
                        .HasOne(b => b.Room)
                        .WithMany(r => r.Bookings)
                        .HasForeignKey(b => b.RoomGuid);

            //One Employee to One Account
            modelBuilder.Entity<Account>()
                        .HasOne(a => a.Employee)
                        .WithOne(emp => emp.Account)
                        .HasForeignKey<Employee>(emp => emp.Guid);

            //many account role to one account
            modelBuilder.Entity<AccountRole>()
                        .HasOne(ar => ar.Account)
                        .WithMany(a => a.AccountRoles)
                        .HasForeignKey(ar => ar.Guid);

            //Many account role to one role
            modelBuilder.Entity<AccountRole>()
                        .HasOne(ar => ar.Role)
                        .WithMany(r => r.AccountRoles)
                        .HasForeignKey(ar => ar.Guid);




        }

    }

}
