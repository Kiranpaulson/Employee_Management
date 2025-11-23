using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException; // For NotFoundException
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler;
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class DeleteDepartmentCommandHandlerTests
{
    private readonly Mock<IDepartmentRepository> _mockRepo;
    private readonly Mock<IAppLogger<DeleteDepartmentCommandHandler>> _mockLogger;

    public DeleteDepartmentCommandHandlerTests()
    {
        _mockRepo = new Mock<IDepartmentRepository>();
        _mockLogger = new Mock<IAppLogger<DeleteDepartmentCommandHandler>>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenDepartmentNotFound()
    {
        // Arrange
        var command = new DeleteDepartmentCommand(99);

        _mockRepo
            .Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Department)null);

        var handler = new DeleteDepartmentCommandHandler(_mockRepo.Object, _mockLogger.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(command, CancellationToken.None));

        _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldDeleteDepartment_AndReturnSuccessMessage()
    {
        // Arrange
        var dept = new Department { Id = 10, Name = "HR" };
        var command = new DeleteDepartmentCommand(10);

        _mockRepo.Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(dept);
        _mockRepo.Setup(r => r.DeleteAsync(dept, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(dept);

        var handler = new DeleteDepartmentCommandHandler(_mockRepo.Object, _mockLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal("Department deleted successfully", result.Message);
        _mockRepo.Verify(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()), Times.Once);
        _mockRepo.Verify(r => r.DeleteAsync(dept, It.IsAny<CancellationToken>()), Times.Once);
    }
}
