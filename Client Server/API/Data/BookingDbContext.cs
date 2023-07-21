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

            // University - Education (One to Many)
            modelBuilder.Entity<University>()
                          .HasMany(u => u.Educations)
                          .WithOne(e => e.University)
                          .HasForeignKey(e => e.UniversityGuid);

            // Room - Booking (One to Many)
            modelBuilder.Entity<Room>()
                        .HasMany(r => r.Bookings)
                        .WithOne(b => b.Room)
                        .HasForeignKey(b => b.RoomGuid);

            // Employee - Booking (One to Many)
            modelBuilder.Entity<Employee>()
                        .HasMany(emp => emp.Bookings)
                        .WithOne(b => b.Employee)
                        .HasForeignKey(b => b.EmployeeGuid);

            // Role - AccountRole (One to Many)
            modelBuilder.Entity<Role>()
                        .HasMany(r => r.AccountRoles)
                        .WithOne(ar => ar.Role)
                        .HasForeignKey(ar => ar.RoleGuid);

            // Account - AccountRole (One to Many)
            modelBuilder.Entity<Account>()
                        .HasMany(a => a.AccountRoles)
                        .WithOne(ar => ar.Account)
                        .HasForeignKey(ar => ar.AccountGuid);

            // Education - Employee (One to One)
            modelBuilder.Entity<Education>()
                        .HasOne(e => e.Employee)
                        .WithOne(emp => emp.Education)
                        .HasForeignKey<Education>(e => e.Guid);

            // Account - Employee (One to One)
            modelBuilder.Entity<Account>()
                       .HasOne(a => a.Employee)
                       .WithOne(emp => emp.Account)
                       .HasForeignKey<Account>(a => a.Guid);

        }

    }

}
