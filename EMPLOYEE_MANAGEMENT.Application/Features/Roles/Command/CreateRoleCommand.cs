using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Command to create a new role.
    /// </summary>
    public class CreateRoleCommand : IRequest<ApiResponse<RoleDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
