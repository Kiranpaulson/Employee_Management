using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using MediatR;
using System.Collections.Generic;

namespace EMPLOYEE_MANAGEMENT.Application.Features.Roles.Query
{
    /// <summary>
    /// Query to retrieve all roles.
    /// </summary>
    public class GetAllRolesQuery : IRequest<ApiResponse<List<RoleDto>>>
    {
    }
}
