using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
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
    public class CreateEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateEmployee_AndReturnCreatedResponse()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                Name = "John Doe",
                PhoneNumber = "9876543210",
                AadharNumber = "123412341234",
                RoleId = 3,
                UserId = 7,
                DepartmentId = 2
            };

            // The entity AutoMapper will generate
            var mappedEntity = new Employee
            {
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
                AadharNumber = command.AadharNumber,
                RoleId = command.RoleId,
                UserId = command.UserId,
                DepartmentId = command.DepartmentId
            };

            // Saved entity returned from repo
            var savedEmployee = new Employee
            {
                Id = 10,
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
                AadharNumber = command.AadharNumber,
                RoleId = command.RoleId,
                UserId = command.UserId,
                DepartmentId = command.DepartmentId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo
                .Setup(r => r.CreateAsync(It.IsAny<Employee>()))
                .ReturnsAsync(savedEmployee);

            var mockMapper = new Mock<IMapper>();

            // mapper: command → entity
            mockMapper
                .Setup(m => m.Map<Employee>(command))
                .Returns(mappedEntity);

            // mapper: entity → dto
            mockMapper
                .Setup(m => m.Map<EmployeeDto>(savedEmployee))
                .Returns(new EmployeeDto
                {
                    Id = savedEmployee.Id,
                    Name = savedEmployee.Name,
                });

            var handler = new CreateEmployeeCommandHandler(mockRepo.Object, mockMapper.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.Created, response.Status);
            Assert.Equal("Employee created successfully", response.Message);
            Assert.NotNull(response.Data);
            Assert.Equal(10, response.Data.Id);
            Assert.Equal("John Doe", response.Data.Name);

            // Verify mapping calls
            mockMapper.Verify(m => m.Map<Employee>(command), Times.Once);
            mockMapper.Verify(m => m.Map<EmployeeDto>(savedEmployee), Times.Once);

            // Verify repository call
            mockRepo.Verify(r => r.CreateAsync(It.IsAny<Employee>()), Times.Once);
        }
    }
}
