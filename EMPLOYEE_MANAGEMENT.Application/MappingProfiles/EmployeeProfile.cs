using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command;
using EMPLOYEE_MANAGEMENT.Domain.Entities;

namespace EMPLOYEE_MANAGEMENT.Application.MappingProfiles
{
    /// <summary>
    /// Defines AutoMapper mappings for employee-related entities and DTOs.
    /// </summary>
    public class EmployeeProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeProfile"/> class
        /// and configures all employee-related AutoMapper mappings.
        /// </summary>
        public EmployeeProfile()
        {
            /// <summary>
            /// Maps <see cref="Employee"/> entity to <see cref="EmployeeDto"/>,
            /// including related navigation properties such as Department, User, and Role.
            /// </summary>
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            /// <summary>
            /// Maps <see cref="CreateEmployeeCommand"/> to <see cref="Employee"/> entity.
            /// </summary>
            CreateMap<CreateEmployeeCommand, Employee>();
        }
    }
}
