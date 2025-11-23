using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Employees.Query
{
    public class GetAllEmployeesQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAppLogger<GetAllEmployeesQueryHandler>> _loggerMock;
        private readonly GetAllEmployeesQueryHandler _handler;

        public GetAllEmployeesQueryHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<IAppLogger<GetAllEmployeesQueryHandler>>();

            _handler = new GetAllEmployeesQueryHandler(
                _employeeRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedEmployeeDtos_WhenEmployeesExist()
        {
            // Arrange
            var employees = new List<Domain.Entities.Employee>
            {
                new Domain.Entities.Employee { Id = 1, Name = "John Doe" },
                new Domain.Entities.Employee { Id = 2, Name = "Jane Smith" }
            };

            var employeeDtos = new List<EmployeeDto>
            {
                new EmployeeDto { Id = 1, Name = "John Doe" },
                new EmployeeDto { Id = 2, Name = "Jane Smith" }
            };

            _employeeRepositoryMock
                .Setup(repo => repo.GetEmployeesWithRelationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(employees);

            _mapperMock
                .Setup(mapper => mapper.Map<List<EmployeeDto>>(employees))
                .Returns(employeeDtos);

            var query = new GetAllEmployeesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.Status);
            Assert.Equal(employeeDtos.Count, result.Data.Count);

            // Single-step equivalence check for first employee
            Assert.Equal(
                new { Id = 1, Name = "John Doe" },
                new { result.Data[0].Id, result.Data[0].Name }
            );

            _employeeRepositoryMock.Verify(repo => repo.GetEmployeesWithRelationsAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<List<EmployeeDto>>(employees), Times.Once);

            // Optional: Verify logger is not null / invoked (depending on handler implementation)
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoEmployeesExist()
        {
            // Arrange
            var employees = new List<Domain.Entities.Employee>();
            var employeeDtos = new List<EmployeeDto>();

            _employeeRepositoryMock
                .Setup(repo => repo.GetEmployeesWithRelationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(employees);

            _mapperMock
                .Setup(mapper => mapper.Map<List<EmployeeDto>>(employees))
                .Returns(employeeDtos);

            var query = new GetAllEmployeesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.Status);
            Assert.Empty(result.Data);

            _employeeRepositoryMock.Verify(repo => repo.GetEmployeesWithRelationsAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<List<EmployeeDto>>(employees), Times.Once);
            _loggerMock.VerifyNoOtherCalls();
        }
    }
}
