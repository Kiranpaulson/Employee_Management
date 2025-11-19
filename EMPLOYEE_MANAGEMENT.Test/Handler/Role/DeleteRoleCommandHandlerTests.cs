using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Roles.Command
{
    public class DeleteRoleCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldDeleteRole_AndReturnSuccess()
        {
            // Arrange
            int id = 3;
            var command = new DeleteRoleCommand(id);

            var role = new Role { Id = id, Name = "Admin" };

            var mockRepo = new Mock<IRoleRepository>();

            // GetById → Task<Role>
            mockRepo
                .Setup(r => r.GetById(id))
                .ReturnsAsync(role);

            // DeleteAsync → Task<Role>
            mockRepo
                .Setup(r => r.DeleteAsync(role))
                .ReturnsAsync(role);

            var handler = new DeleteRoleCommandHandler(mockRepo.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.OK, response.Status);
            Assert.Equal("Role deleted successfully", response.Data);

            mockRepo.Verify(r => r.GetById(id), Times.Once);
            mockRepo.Verify(r => r.DeleteAsync(role), Times.Once);
        }


        [Fact]
        public async Task Handle_ShouldReturnFail_WhenRoleNotFound()
        {
            // Arrange
            int id = 999;
            var command = new DeleteRoleCommand(id);

            var mockRepo = new Mock<IRoleRepository>();

            // GetById returns null correctly
            mockRepo
                .Setup(r => r.GetById(id))
                .ReturnsAsync((Role)null);

            var handler = new DeleteRoleCommandHandler(mockRepo.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.BadRequest, response.Status);
            Assert.Equal("Role not found", response.Message);
            Assert.Null(response.Data);

            mockRepo.Verify(r => r.GetById(id), Times.Once);
            mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Role>()), Times.Never);
        }
    }
}
