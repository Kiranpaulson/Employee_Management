using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Roles.Command
{
    public class UpdateRoleCommandHandlerTests
    {
        private readonly Mock<IRoleRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IAppLogger<UpdateRoleCommandHandler>> _mockLogger;

        public UpdateRoleCommandHandlerTests()
        {
            _mockRepo = new Mock<IRoleRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<IAppLogger<UpdateRoleCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ShouldUpdateRole_AndReturnUpdatedDto()
        {
            // Arrange
            int roleId = 2;

            var command = new UpdateRoleCommand
            {
                Id = roleId,
                Name = "Updated Name",
                Description = "Updated Description"
            };

            var existingRole = new Role
            {
                Id = roleId,
                Name = "Old Name",
                Description = "Old Description",
                UpdatedDate = DateTime.UtcNow.AddDays(-1)
            };

            var updatedRole = new Role
            {
                Id = roleId,
                Name = command.Name,
                Description = command.Description,
                UpdatedDate = DateTime.UtcNow
            };

            // Mock repository
            _mockRepo.Setup(r => r.GetByIdAsync(roleId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(existingRole);
            _mockRepo.Setup(r => r.UpdateAsync(existingRole, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(updatedRole);

            // Mock mapper
            _mockMapper.Setup(m => m.Map<RoleDto>(updatedRole))
                       .Returns(new RoleDto
                       {
                           Id = roleId,
                           Name = command.Name,
                           Description = command.Description
                       });

            var handler = new UpdateRoleCommandHandler(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.OK, response.Status);
            Assert.Equal(roleId, response.Data.Id);
            Assert.Equal("Updated Name", response.Data.Name);
            Assert.Equal("Updated Description", response.Data.Description);

            _mockRepo.Verify(r => r.GetByIdAsync(roleId, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.UpdateAsync(existingRole, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<RoleDto>(updatedRole), Times.Once);
            _mockLogger.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenRoleDoesNotExist()
        {
            // Arrange
            int roleId = 999;
            var command = new UpdateRoleCommand
            {
                Id = roleId,
                Name = "Whatever"
            };

            _mockRepo.Setup(r => r.GetByIdAsync(roleId, It.IsAny<CancellationToken>()))
                     .ReturnsAsync((Role)null);

            var handler = new UpdateRoleCommandHandler(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

            // Act + Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));

            Assert.Equal($"Role with Id {roleId} not found", exception.Message);

            _mockRepo.Verify(r => r.GetByIdAsync(roleId, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Role>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockLogger.VerifyNoOtherCalls();
        }
    }
}
