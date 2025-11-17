using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Command;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Domain.Entities;

namespace EMPLOYEE_MANAGEMENT.Application.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Map Employee → EmployeeDto
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));

            CreateMap<CreateEmployeeCommand, Employee>();
        }
    }
}
