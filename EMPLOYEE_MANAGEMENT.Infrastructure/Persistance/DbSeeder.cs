using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Persistance
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;

            // ============================
            // 🔹 SEED ROLES
            // ============================
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Administrator role", CreatedDate = now, UpdatedDate = now },
                new Role { Id = 2, Name = "Employee", Description = "Employee role", CreatedDate = now, UpdatedDate = now },
                new Role { Id = 3, Name = "HR Manager", Description = "Manages HR operations", CreatedDate = now, UpdatedDate = now },
                new Role { Id = 4, Name = "Software Engineer", Description = "Responsible for development", CreatedDate = now, UpdatedDate = now }
            );

            // ============================
            // 🔹 SEED DEPARTMENTS
            // ============================
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = 1,
                    Name = "Human Resources",
                    Description = "Handles employee relations and recruitment.",
                    CreatedDate = now,
                    UpdatedDate = now
                },
                new Department
                {
                    Id = 2,
                    Name = "IT Department",
                    Description = "Responsible for technical systems and support.",
                    CreatedDate = now,
                    UpdatedDate = now
                }
            );

            // ============================
            // 🔹 SEED USERS
            // ============================
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

            // ============================
            // 🔹 SEED EMPLOYEES
            // ============================
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    DepartmentId = 1,
                    UserId = 1,
                    RoleId = 1,
                    PhoneNumber = "9876543210",
                    AadharNumber = "123456789012",
                    CreatedDate = now,
                    UpdatedDate = now
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    DepartmentId = 2,
                    UserId = 2,
                    RoleId = 2,
                    PhoneNumber = "8765432109",
                    AadharNumber = "987654321098",
                    CreatedDate = now,
                    UpdatedDate = now
                }
            );
        }
    }
}
