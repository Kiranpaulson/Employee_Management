using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
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
    public class CreateRoleCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateRole_AndReturnCreatedResponse()
        {
            // Arrange
            var command = new CreateRoleCommand
            {
                Name = "Manager",
                Description = "Manages employees"
            };

            var mappedRole = new Role
            {
                Name = command.Name,
                Description = command.Description
            };

            var savedRole = new Role
            {
                Id = 10,
                Name = command.Name,
                Description = command.Description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var mockRepo = new Mock<IRoleRepository>();

            mockRepo
                .Setup(r => r.CreateAsync(It.IsAny<Role>()))
                .ReturnsAsync(savedRole);

            var mockMapper = new Mock<IMapper>();

            // Map CreateRoleCommand → Role
            mockMapper
                .Setup(m => m.Map<Role>(command))
                .Returns(mappedRole);

            // Map Role → RoleDto
            mockMapper
                .Setup(m => m.Map<RoleDto>(savedRole))
                .Returns(new RoleDto
                {
                    Id = savedRole.Id,
                    Name = savedRole.Name,
                    Description = savedRole.Description
                });

            var handler = new CreateRoleCommandHandler(mockRepo.Object, mockMapper.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.Created, response.Status);
            Assert.Equal("Role created successfully", response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(10, response.Data.Id);
            Assert.Equal("Manager", response.Data.Name);

            // Verify interactions
            mockMapper.Verify(m => m.Map<Role>(command), Times.Once);
            mockMapper.Verify(m => m.Map<RoleDto>(savedRole), Times.Once);
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<Role>()), Times.Once);
        }
    }
}
