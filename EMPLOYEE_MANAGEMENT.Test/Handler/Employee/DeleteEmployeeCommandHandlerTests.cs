using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Employees.Command
{
    public class DeleteEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly Mock<IAppLogger<DeleteEmployeeCommandHandler>> _mockLogger;

        public DeleteEmployeeCommandHandlerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _mockLogger = new Mock<IAppLogger<DeleteEmployeeCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ShouldDeleteEmployee_AndReturnSuccess()
        {
            // Arrange
            int id = 5;
            var command = new DeleteEmployeeCommand(id);
            var employee = new Employee { Id = id, Name = "Test User" };

            _mockRepo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(employee);
            _mockRepo.Setup(r => r.DeleteAsync(employee, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(employee);

            var handler = new DeleteEmployeeCommandHandler(_mockRepo.Object, _mockLogger.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.OK, response.Status);
            Assert.Equal("Employee deleted successfully", response.Data);

            _mockRepo.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.DeleteAsync(employee, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenEmployeeNotFound()
        {
            // Arrange
            int id = 999;
            var command = new DeleteEmployeeCommand(id);

            _mockRepo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Employee)null);

            var handler = new DeleteEmployeeCommandHandler(_mockRepo.Object, _mockLogger.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.BadRequest, response.Status);
            Assert.Equal("Employee not found", response.Message);
            Assert.Null(response.Data);

            _mockRepo.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
