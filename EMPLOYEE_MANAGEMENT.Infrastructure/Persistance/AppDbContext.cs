using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Persistance
{
    /// <summary>
    /// Represents the Entity Framework Core database context for the Employee Management system.
    /// Handles entity configurations, relationships, and database access.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class
        /// using the provided database context options.
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for managing employee records.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// DbSet for managing department records.
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// DbSet for managing user accounts.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// DbSet for managing role information.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Configures entity relationships, constraints,
        /// and seeds initial data using model builder.
        /// </summary>
        /// <param name="modelBuilder">EF Core model builder used to configure entity mappings.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ============================
            // 🔗 RELATIONSHIPS
            // ============================

            /// <summary>
            /// Configure 1-to-1 relationship: Employee ↔ User.
            /// </summary>
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            /// <summary>
            /// Configure 1-to-many relationship: Department → Employees.
            /// </summary>
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            /// <summary>
            /// Configure many-to-1 relationship: Role → Employees.
            /// </summary>
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // 🔹 SEED DATA FROM SEEDER
            // ============================

            /// <summary>
            /// Calls the extension method to seed initial data.
            /// </summary>
            modelBuilder.Seed();
        }
    }
}
