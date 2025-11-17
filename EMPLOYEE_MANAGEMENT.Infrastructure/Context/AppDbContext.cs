using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee ↔ User (One-to-One)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Department ↔ Employee (One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // =======================
            // 🔹 Seed Data
            // =======================

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Human Resources", Description = "Handles employee relations and recruitment." },
                new Department { Id = 2, Name = "IT Department", Description = "Responsible for technical systems and support." }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "john.doe",
                    Email = "john.doe@example.com",
                    PasswordHash = "hashedpassword123",
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    Username = "jane.smith",
                    Email = "jane.smith@example.com",
                    PasswordHash = "hashedpassword456",
                    IsActive = true
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Role = "HR Manager",
                    DepartmentId = 1,
                    UserId = 1,
                    PhoneNumber = "9876543210",
                    AadharNumber = "123456789012"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Role = "Software Engineer",
                    DepartmentId = 2,
                    UserId = 2,
                    PhoneNumber = "8765432109",
                    AadharNumber = "987654321098"
                }
            );
        }
    }
}
