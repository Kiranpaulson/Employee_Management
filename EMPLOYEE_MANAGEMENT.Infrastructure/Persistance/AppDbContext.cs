using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Persistance
{
    /// <summary>
    /// Represents the database context for the Employee Management system.
    /// Manages all entity configurations, DbSets, and database interactions using Entity Framework Core.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class
        /// using the provided database configuration options.
        /// </summary>
        /// <param name="options">The database context configuration options.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Employees table.
        /// Used for CRUD operations on employee records.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the Departments table.
        /// Stores all department details used in the system.
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Gets or sets the Users table.
        /// Used for storing and managing authentication-related data.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Roles table.
        /// Defines different role types assigned to users or employees.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Configures entity relationships, property constraints, and database mappings
        /// using the Fluent API. Overrides the default EF Core behavior.
        /// </summary>
        /// <param name="modelBuilder">The builder used to configure the entity model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================
            // Department Config
            // ==========================
            modelBuilder.Entity<Department>(entity =>
            {
                /// <summary>
                /// Configures the Department entity including primary key,
                /// property constraints, and model validation rules.
                /// </summary>

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasMaxLength(250);
            });

            // ==========================
            // Role Config
            // ==========================
            modelBuilder.Entity<Role>(entity =>
            {
                /// <summary>
                /// Configures the Role entity including key, validation,
                /// and maximum property lengths.
                /// </summary>

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasMaxLength(250);
            });

            // ==========================
            // User Config
            // ==========================
            modelBuilder.Entity<User>(entity =>
            {
                /// <summary>
                /// Configures the User entity including unique constraints,
                /// required fields, and maximum lengths.
                /// </summary>

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .IsRequired();
                    
            });

            // ==========================
            // Employee Config
            // ==========================
            modelBuilder.Entity<Employee>(entity =>
            {
                /// <summary>
                /// Configures the Employee entity including relationships,
                /// foreign keys, and property validation.
                /// </summary>

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10);

                entity.Property(e => e.AadharNumber)
                    .HasMaxLength(12);

                // ---------- RELATIONSHIPS -----------

                // Department → Employees (1-to-many)
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Role → Employees (1-to-many)
                entity.HasOne(e => e.Role)
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);

                // User ↔ Employee (1-to-1)
                entity.HasOne(e => e.User)
                    .WithOne(u => u.Employee)
                    .HasForeignKey<Employee>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Seeds();
        }
    }
}
