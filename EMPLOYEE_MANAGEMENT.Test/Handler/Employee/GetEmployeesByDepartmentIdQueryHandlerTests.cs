using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Employees.Query
{
    public class GetEmployeesByDepartmentIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnEmployeesFilteredByDepartment()
        {
            // Arrange
            var departmentId = 1;

            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John", DepartmentId = 1 },
                new Employee { Id = 2, Name = "Alice", DepartmentId = 2 },
                new Employee { Id = 3, Name = "Bob", DepartmentId = 1 }
            };

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo
                .Setup(repo => repo.GetEmployeesWithRelationsAsync())
                .ReturnsAsync(employees);

            var mockMapper = new Mock<IMapper>();
            mockMapper
                .Setup(m => m.Map<List<EmployeeDto>>(It.IsAny<List<Employee>>()))
                .Returns((List<Employee> src) =>
                    src.Select(e => new EmployeeDto { Id = e.Id, Name = e.Name }).ToList()
                );

            var handler = new GetEmployeesByDepartmentIdQueryHandler(mockRepo.Object, mockMapper.Object);
            var request = new GetEmployeesByDepartmentIdQuery(departmentId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.Status);   // <- use Status
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            Assert.Contains(result.Data, e => e.Id == 1);
            Assert.Contains(result.Data, e => e.Id == 3);
            Assert.DoesNotContain(result.Data, e => e.Id == 2);

            // Ensure repository method is called exactly once
            mockRepo.Verify(r => r.GetEmployeesWithRelationsAsync(), Times.Once);
            mockMapper.Verify(m => m.Map<List<EmployeeDto>>(It.IsAny<List<Employee>>()), Times.Once);
        }
    }
}
