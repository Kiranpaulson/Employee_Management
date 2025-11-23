using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Command;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler;
using EMPLOYEE_MANAGEMENT.Application.CustomException; // For NotFoundException
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
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
        private readonly Mock<IAppLogger<UpdateDepartmentCommandHandler>> _mockLogger;

        public UpdateDepartmentCommandHandlerTests()
        {
            _mockRepo = new Mock<IDepartmentRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<IAppLogger<UpdateDepartmentCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenDepartmentNotFound()
        {
            // Arrange
            var command = new UpdateDepartmentCommand
            {
                Id = 99,
                Name = "NewName",
                Description = "NewDesc"
            };

            _mockRepo
                .Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Department)null);

            var handler = new UpdateDepartmentCommandHandler(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None));

            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()), Times.Never);
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

            _mockRepo.Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(existing);

            _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(updatedDept);

            _mockMapper.Setup(m => m.Map<DepartmentDto>(It.IsAny<Department>()))
                       .Returns(dto);

            var handler = new UpdateDepartmentCommandHandler(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert using single-step equivalence
            Assert.Equal(
                new { Id = 10, Name = "HR Updated", Description = "Updated Desc" },
                new { result.Data.Id, result.Data.Name, result.Data.Description }
            );

            _mockRepo.Verify(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<DepartmentDto>(It.IsAny<Department>()), Times.Once);
        }
    }
}
