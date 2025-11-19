using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command
{
    /// <summary>
    /// Command to update an existing role.
    /// </summary>
    public class UpdateRoleCommand : IRequest<ApiResponse<RoleDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
