using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

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
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================
            // 🔗 RELATIONSHIPS
            // ============================

            // Employee ↔ User (One-to-One)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Employee ↔ Department (Many-to-One)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee ↔ Role (Many-to-One)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // 🔹 SEED DATA
            // ============================

            var now = DateTime.UtcNow;

            // Seed Roles
           

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Administrator role", CreatedDate = now, UpdatedDate = now },
                new Role { Id = 2, Name = "Employee", Description = "Employee role", CreatedDate = now, UpdatedDate = now },

                new Role { Id = 3, Name = "HR Manager", Description = "Manages HR operations", CreatedDate = now, UpdatedDate = now },
                new Role { Id = 4, Name = "Software Engineer", Description = "Responsible for development", CreatedDate = now, UpdatedDate = now }
            );


            // Seed Departments
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

            // Seed Users
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

            // Seed Employees
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
