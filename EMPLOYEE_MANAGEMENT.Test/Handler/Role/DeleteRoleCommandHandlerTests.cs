using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Roles.Command
{
    public class DeleteRoleCommandHandlerTests
    {
        private readonly Mock<IRoleRepository> _mockRepo;
        private readonly Mock<IAppLogger<DeleteRoleCommandHandler>> _mockLogger;

        public DeleteRoleCommandHandlerTests()
        {
            _mockRepo = new Mock<IRoleRepository>();
            _mockLogger = new Mock<IAppLogger<DeleteRoleCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ShouldDeleteRole_AndReturnSuccess()
        {
            // Arrange
            int id = 3;
            var command = new DeleteRoleCommand(id);
            var role = new Role { Id = id, Name = "Admin" };

            _mockRepo
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(role);

            _mockRepo
                .Setup(r => r.DeleteAsync(role, It.IsAny<CancellationToken>()))
                .ReturnsAsync(role);

            var handler = new DeleteRoleCommandHandler(_mockRepo.Object, _mockLogger.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.OK, response.Status);
            Assert.Equal("Role deleted successfully", response.Data);

            _mockRepo.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.DeleteAsync(role, It.IsAny<CancellationToken>()), Times.Once);
            _mockLogger.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenRoleNotFound()
        {
            // Arrange
            int id = 999;
            var command = new DeleteRoleCommand(id);

            _mockRepo
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Role)null);

            var handler = new DeleteRoleCommandHandler(_mockRepo.Object, _mockLogger.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.BadRequest, response.Status);
            Assert.Equal("Role not found", response.Message);
            Assert.Null(response.Data);

            _mockRepo.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Role>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockLogger.VerifyNoOtherCalls();
        }
    }
}
