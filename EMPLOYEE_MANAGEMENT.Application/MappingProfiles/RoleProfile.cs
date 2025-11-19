using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Domain.Entities;

namespace EMPLOYEE_MANAGEMENT.Application.MappingProfiles
{
    /// <summary>
    /// AutoMapper profile for Role entity, DTO, and commands.
    /// </summary>
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleCommand, Role>();
            CreateMap<UpdateRoleCommand, Role>();
        }
    }
}
