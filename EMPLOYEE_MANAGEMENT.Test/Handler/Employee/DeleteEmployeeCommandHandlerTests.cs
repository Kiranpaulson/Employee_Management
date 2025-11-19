using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Employees.Command
{
    public class DeleteEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteEmployee_AndReturnSuccess()
        {
            // Arrange
            int id = 5;
            var command = new DeleteEmployeeCommand(id);

            var employee = new Employee { Id = id, Name = "Test User" };

            var mockRepo = new Mock<IEmployeeRepository>();

            // Correct return type → Task<Employee>
            mockRepo
                .Setup(r => r.GetById(id))
                .ReturnsAsync(employee);

            // DeleteAsync returns Task → return Task.CompletedTask ONLY (this never conflicts)
            mockRepo
       .Setup(r => r.DeleteAsync(employee))
       .ReturnsAsync(employee);

            var handler = new DeleteEmployeeCommandHandler(mockRepo.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.OK, response.Status);
            Assert.Equal("Employee deleted successfully", response.Data);

            mockRepo.Verify(r => r.GetById(id), Times.Once);
            mockRepo.Verify(r => r.DeleteAsync(employee), Times.Once);
        }


        [Fact]
        public async Task Handle_ShouldReturnFail_WhenEmployeeNotFound()
        {
            // Arrange
            int id = 999;
            var command = new DeleteEmployeeCommand(id);

            var mockRepo = new Mock<IEmployeeRepository>();

            // Return null correctly → Task<Employee>
            mockRepo
                .Setup(r => r.GetById(id))
                .ReturnsAsync((Employee)null);

            var handler = new DeleteEmployeeCommandHandler(mockRepo.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.BadRequest, response.Status);
            Assert.Equal("Employee not found", response.Message);
            Assert.Null(response.Data);

            mockRepo.Verify(r => r.GetById(id), Times.Once);
            mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Employee>()), Times.Never);
        }
    }
}
