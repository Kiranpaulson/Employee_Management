using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
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
        private readonly GetEmployeeByIdQueryHandler _handler;

        public GetEmployeeByIdQueryHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new GetEmployeeByIdQueryHandler(
                _employeeRepositoryMock.Object,
                _mapperMock.Object
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
                .Setup(repo => repo.GetEmployeeWithRelationsByIdAsync(employeeId))
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
            Assert.Equal(employeeDto.Id, result.Data.Id);
            Assert.Equal(employeeDto.Name, result.Data.Name);

            _employeeRepositoryMock.Verify(repo => repo.GetEmployeeWithRelationsByIdAsync(employeeId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<EmployeeDto>(employee), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = 99;

            _employeeRepositoryMock
                .Setup(repo => repo.GetEmployeeWithRelationsByIdAsync(employeeId))
                .ReturnsAsync((Domain.Entities.Employee)null);

            var query = new GetEmployeeByIdQuery(employeeId);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None));

            Assert.Equal($"Employee with Id {employeeId} not found", exception.Message);
            _employeeRepositoryMock.Verify(repo => repo.GetEmployeeWithRelationsByIdAsync(employeeId), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<EmployeeDto>(It.IsAny<Domain.Entities.Employee>()), Times.Never);
        }
    }
}
