using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
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

            var mockRepo = new Mock<IRoleRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepo
                .Setup(r => r.GetById(roleId))
                .ReturnsAsync(existingRole);

            mockRepo
                .Setup(r => r.UpdateAsync(existingRole))
                .ReturnsAsync(updatedRole);

            mockMapper
                .Setup(m => m.Map<RoleDto>(existingRole))
                .Returns(new RoleDto
                {
                    Id = roleId,
                    Name = command.Name,
                    Description = command.Description
                });

            var handler = new UpdateRoleCommandHandler(mockRepo.Object, mockMapper.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(StatusCode.OK, response.Status);
            Assert.Equal(roleId, response.Data.Id);
            Assert.Equal("Updated Name", response.Data.Name);
            Assert.Equal("Updated Description", response.Data.Description);

            mockRepo.Verify(r => r.GetById(roleId), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(existingRole), Times.Once);
            mockMapper.Verify(m => m.Map<RoleDto>(existingRole), Times.Once);
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

            var mockRepo = new Mock<IRoleRepository>();
            var mockMapper = new Mock<IMapper>();

            // Return null
            mockRepo
                .Setup(r => r.GetById(roleId))
                .ReturnsAsync((Role)null);

            var handler = new UpdateRoleCommandHandler(mockRepo.Object, mockMapper.Object);

            // Act + Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));

            mockRepo.Verify(r => r.GetById(roleId), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Role>()), Times.Never);
        }
    }
}
