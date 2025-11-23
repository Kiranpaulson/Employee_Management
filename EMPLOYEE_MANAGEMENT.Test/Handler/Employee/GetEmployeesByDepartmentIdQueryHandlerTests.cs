using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
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
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IAppLogger<GetEmployeesByDepartmentIdQueryHandler>> _loggerMock;

        public GetEmployeesByDepartmentIdQueryHandlerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _loggerMock = new Mock<IAppLogger<GetEmployeesByDepartmentIdQueryHandler>>();
        }

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

            _mockRepo
                .Setup(repo => repo.GetEmployeesWithRelationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(employees);

            _mockMapper
                .Setup(m => m.Map<List<EmployeeDto>>(It.IsAny<List<Employee>>()))
                .Returns((List<Employee> src) =>
                    src.Select(e => new EmployeeDto { Id = e.Id, Name = e.Name }).ToList()
                );

            var handler = new GetEmployeesByDepartmentIdQueryHandler(_mockRepo.Object, _mockMapper.Object, _loggerMock.Object);
            var request = new GetEmployeesByDepartmentIdQuery(departmentId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);

            // Single-step equivalence for included employees
            var expectedEmployees = new[]
            {
                new { Id = 1, Name = "John" },
                new { Id = 3, Name = "Bob" }
            };

            foreach (var expected in expectedEmployees)
            {
                Assert.Contains(result.Data, e => e.Id == expected.Id && e.Name == expected.Name);
            }

            // Ensure excluded employee is not present
            Assert.DoesNotContain(result.Data, e => e.Id == 2);

            // Verify mocks
            _mockRepo.Verify(r => r.GetEmployeesWithRelationsAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<List<EmployeeDto>>(It.IsAny<List<Employee>>()), Times.Once);
            _loggerMock.VerifyNoOtherCalls();
        }
    }
}
