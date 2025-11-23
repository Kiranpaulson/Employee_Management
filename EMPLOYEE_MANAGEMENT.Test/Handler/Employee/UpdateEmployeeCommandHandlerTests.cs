using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Employees.Command
{
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IAppLogger<UpdateEmployeeCommandHandler>> _mockLogger;

        public UpdateEmployeeCommandHandlerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<IAppLogger<UpdateEmployeeCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ShouldUpdateEmployee_AndReturnUpdatedResponse()
        {
            // Arrange
            var command = new UpdateEmployeeCommand
            {
                Id = 5,
                Name = "Updated Name",
                PhoneNumber = "9999999999",
                AadharNumber = "123412341234",
                RoleId = 2,
                UserId = 4,
                DepartmentId = 3
            };

            var existingEmployee = new Employee
            {
                Id = 5,
                Name = "Old Name",
                PhoneNumber = "1111111111",
                AadharNumber = "000000000000",
                RoleId = 1,
                UserId = 1,
                DepartmentId = 1,
                CreatedDate = DateTime.UtcNow.AddDays(-10),
                UpdatedDate = DateTime.UtcNow.AddDays(-10)
            };

            _mockRepo
                .Setup(r => r.GetByIdAsync(5, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingEmployee);

            _mockRepo
                .Setup(r => r.UpdateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Employee e, CancellationToken _) => e);

            _mockMapper
                .Setup(m => m.Map<EmployeeDto>(It.IsAny<Employee>()))
                .Returns((Employee e) => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber
                });

            var handler = new UpdateEmployeeCommandHandler(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.OK, response.Status);
            Assert.Equal("Employee updated successfully", response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(5, response.Data.Id);
            Assert.Equal("Updated Name", response.Data.Name);
            Assert.Equal("9999999999", response.Data.PhoneNumber);

            _mockRepo.Verify(r => r.GetByIdAsync(5, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<EmployeeDto>(It.IsAny<Employee>()), Times.Once);
            _mockLogger.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var command = new UpdateEmployeeCommand
            {
                Id = 999,
                Name = "Anything"
            };

            _mockRepo
                .Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Employee)null);

            var handler = new UpdateEmployeeCommandHandler(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None)
            );

            Assert.Equal($"Employee with Id {command.Id} not found", exception.Message);

            _mockRepo.Verify(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockMapper.Verify(m => m.Map<EmployeeDto>(It.IsAny<Employee>()), Times.Never);
            _mockLogger.VerifyNoOtherCalls();
        }
    }
}
