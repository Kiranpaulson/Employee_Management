using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
using EMPLOYEE_MANAGEMENT.Application.CustomException;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Employees.Command;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
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

            var updatedEmployeeDto = new EmployeeDto
            {
                Id = 5,
                Name = "Updated Name",
                PhoneNumber = "9999999999"
            };

            var mockRepo = new Mock<IEmployeeRepository>();
            var mockMapper = new Mock<IMapper>();

            // repo: find existing employee
            mockRepo
                .Setup(r => r.GetById(5))
                .ReturnsAsync(existingEmployee);

            // repo: update employee (returns the updated entity)
            mockRepo
                .Setup(r => r.UpdateAsync(It.IsAny<Employee>()))
                .ReturnsAsync((Employee e) => e);

            // mapper: entity → dto
            mockMapper
                .Setup(m => m.Map<EmployeeDto>(It.IsAny<Employee>()))
                .Returns((Employee e) => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber
                });

            var handler = new UpdateEmployeeCommandHandler(mockRepo.Object, mockMapper.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.OK, response.Status); // ApiResponse.Success uses OK
            Assert.Equal("Employee updated successfully", response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(5, response.Data.Id);
            Assert.Equal("Updated Name", response.Data.Name);
            Assert.Equal("9999999999", response.Data.PhoneNumber);

            // Verify repository calls
            mockRepo.Verify(r => r.GetById(5), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Employee>()), Times.Once);

            // Verify mapping call
            mockMapper.Verify(m => m.Map<EmployeeDto>(It.IsAny<Employee>()), Times.Once);
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

            var mockRepo = new Mock<IEmployeeRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepo
                .Setup(r => r.GetById(999))
                .ReturnsAsync((Employee)null);

            var handler = new UpdateEmployeeCommandHandler(mockRepo.Object, mockMapper.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                handler.Handle(command, CancellationToken.None)
            );

            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Employee>()), Times.Never);
            mockMapper.Verify(m => m.Map<EmployeeDto>(It.IsAny<Employee>()), Times.Never);
        }
    }
}
