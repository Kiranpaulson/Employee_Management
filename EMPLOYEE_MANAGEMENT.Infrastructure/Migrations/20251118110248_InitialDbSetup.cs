using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMPLOYEE_MANAGEMENT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    AadharNumber = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), "Handles employee relations and recruitment.", "Human Resources", new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622) },
                    { 2, new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), "Responsible for technical systems and support.", "IT Department", new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), "Administrator role", "Admin", new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622) },
                    { 2, new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), "Employee role", "Employee", new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622) },
                    { 3, new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), "Manages HR operations", "HR Manager", new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622) },
                    { 4, new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), "Responsible for development", "Software Engineer", new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", true, "hashedpassword123", "john.doe" },
                    { 2, "jane.smith@example.com", true, "hashedpassword456", "jane.smith" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AadharNumber", "CreatedDate", "DepartmentId", "Name", "PhoneNumber", "RoleId", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, "123456789012", new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), 1, "John Doe", "9876543210", 1, new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), 1 },
                    { 2, "987654321098", new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), 2, "Jane Smith", "8765432109", 2, new DateTime(2025, 11, 18, 11, 2, 47, 656, DateTimeKind.Utc).AddTicks(6622), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
