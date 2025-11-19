using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Departments
{
    public class UpdateDepartmentCommandHandlerTests
    {
        private readonly Mock<IDepartmentRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;

        public UpdateDepartmentCommandHandlerTests()
        {
            _mockRepo = new Mock<IDepartmentRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenDepartmentNotFound()
        {
            // Arrange: create command via object initializer (safe)
            var command = new UpdateDepartmentCommand
            {
                Id = 99,
                Name = "NewName",
                Description = "NewDesc"
            };

            _mockRepo
                .Setup(r => r.GetById(99))
                .ReturnsAsync((Department)null);

            var handler = new UpdateDepartmentCommandHandler(_mockRepo.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Department not found", result.Message);
            Assert.Null(result.Data);

            _mockRepo.Verify(r => r.GetById(99), Times.Once);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Department>()), Times.Never);
            _mockMapper.Verify(m => m.Map<DepartmentDto>(It.IsAny<Department>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldUpdateDepartment_AndReturnDto()
        {
            // Arrange
            var existing = new Department
            {
                Id = 10,
                Name = "Old",
                Description = "Old Desc",
                CreatedDate = DateTime.UtcNow.AddDays(-5),
                UpdatedDate = DateTime.UtcNow.AddDays(-5)
            };

            // Use object initializer for command
            var command = new UpdateDepartmentCommand
            {
                Id = 10,
                Name = "HR Updated",
                Description = "Updated Desc"
            };

            var updatedDept = new Department
            {
                Id = 10,
                Name = "HR Updated",
                Description = "Updated Desc",
                UpdatedDate = DateTime.UtcNow
            };

            var dto = new DepartmentDto
            {
                Id = 10,
                Name = "HR Updated",
                Description = "Updated Desc"
            };

            _mockRepo
                .Setup(r => r.GetById(10))
                .ReturnsAsync(existing);

            _mockRepo
                .Setup(r => r.UpdateAsync(It.IsAny<Department>()))
                .ReturnsAsync(updatedDept);

            _mockMapper
                .Setup(m => m.Map<DepartmentDto>(It.IsAny<Department>()))
                .Returns(dto);

            var handler = new UpdateDepartmentCommandHandler(_mockRepo.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert (check data/message/status as per your ApiResponse)
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(10, result.Data.Id);
            Assert.Equal("HR Updated", result.Data.Name);
            Assert.Equal("Updated Desc", result.Data.Description);

            _mockRepo.Verify(r => r.GetById(10), Times.Once);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Department>()), Times.Once);
            _mockMapper.Verify(m => m.Map<DepartmentDto>(It.IsAny<Department>()), Times.Once);
        }
    }
}
