using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Domain.Entities;

namespace EMPLOYEE_MANAGEMENT.Application.MappingProfiles
{
    /// <summary>
    /// AutoMapper profile for mapping between Department entities, DTOs, and commands.
    /// </summary>
    public class DepartmentProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentProfile"/> class.
        /// Configures mappings for Department-related objects.
        /// </summary>
        public DepartmentProfile()
        {
            // Map Department → DepartmentDto
            CreateMap<Department, DepartmentDto>();

            // Map CreateDepartmentCommand → Department
            CreateMap<CreateDepartmentCommand, Department>();

            // Map UpdateDepartmentCommand → Department (for patch updates)
            CreateMap<UpdateDepartmentCommand, Department>();
        }
    }
}
