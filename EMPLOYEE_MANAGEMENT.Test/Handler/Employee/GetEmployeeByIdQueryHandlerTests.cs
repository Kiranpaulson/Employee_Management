using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Employees.Query
{
    public class GetEmployeeByIdQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAppLogger<GetEmployeeByIdQueryHandler>> _loggerMock;
        private readonly GetEmployeeByIdQueryHandler _handler;

        public GetEmployeeByIdQueryHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<IAppLogger<GetEmployeeByIdQueryHandler>>();

            _handler = new GetEmployeeByIdQueryHandler(
                _employeeRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnEmployeeDto_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = 1;
            var employee = new Domain.Entities.Employee { Id = employeeId, Name = "John Doe" };
            var employeeDto = new EmployeeDto { Id = employeeId, Name = "John Doe" };

            _employeeRepositoryMock
                .Setup(repo => repo.GetEmployeeWithRelationsByIdAsync(employeeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(employee);

            _mapperMock
                .Setup(mapper => mapper.Map<EmployeeDto>(employee))
                .Returns(employeeDto);

            var query = new GetEmployeeByIdQuery(employeeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.Status);
            Assert.Equal(
                new { employeeDto.Id, employeeDto.Name },
                new { result.Data.Id, result.Data.Name }
            );

            _employeeRepositoryMock.Verify(repo => repo.GetEmployeeWithRelationsByIdAsync(employeeId, It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<EmployeeDto>(employee), Times.Once);
            _loggerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = 99;

            _employeeRepositoryMock
                .Setup(repo => repo.GetEmployeeWithRelationsByIdAsync(employeeId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Employee)null);

            var query = new GetEmployeeByIdQuery(employeeId);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None));

            Assert.Equal($"Employee with Id {employeeId} not found", exception.Message);

            _employeeRepositoryMock.Verify(repo => repo.GetEmployeeWithRelationsByIdAsync(employeeId, It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<EmployeeDto>(It.IsAny<Domain.Entities.Employee>()), Times.Never);
            _loggerMock.VerifyNoOtherCalls();
        }
    }
}
