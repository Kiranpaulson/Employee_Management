using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class DeleteDepartmentCommandHandlerTests
{
    private readonly Mock<IDepartmentRepository> _mockRepo = new();

    [Fact]
    public async Task Handle_ShouldReturnFail_WhenDepartmentNotFound()
    {
        // Arrange
        var command = new DeleteDepartmentCommand(99);

        _mockRepo
            .Setup(r => r.GetById(99))
            .ReturnsAsync((Department)null);

        var handler = new DeleteDepartmentCommandHandler(_mockRepo.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal("Department not found", result.Message);
        _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Department>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldDeleteDepartment_AndReturnMessage()
    {
        // Arrange
        var dept = new Department { Id = 10, Name = "HR" };
        var command = new DeleteDepartmentCommand(10);

        _mockRepo.Setup(r => r.GetById(10)).ReturnsAsync(dept);
        _mockRepo.Setup(r => r.DeleteAsync(dept)).ReturnsAsync(dept);

        var handler = new DeleteDepartmentCommandHandler(_mockRepo.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal("Request processed successfully", result.Message);
        _mockRepo.Verify(r => r.GetById(10), Times.Once);
        _mockRepo.Verify(r => r.DeleteAsync(dept), Times.Once);
    }
}
