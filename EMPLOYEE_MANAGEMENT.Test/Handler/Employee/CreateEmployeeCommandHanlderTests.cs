using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Constants;
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
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IAppLogger<CreateEmployeeCommandHandler>> _mockLogger;

        public CreateEmployeeCommandHandlerTests()
        {
            _mockRepo = new Mock<IEmployeeRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<IAppLogger<CreateEmployeeCommandHandler>>();
        }

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

            var mappedEntity = new Employee
            {
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
                AadharNumber = command.AadharNumber,
                RoleId = command.RoleId,
                UserId = command.UserId,
                DepartmentId = command.DepartmentId
            };

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

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(savedEmployee);

            _mockMapper.Setup(m => m.Map<Employee>(command)).Returns(mappedEntity);
            _mockMapper.Setup(m => m.Map<EmployeeDto>(savedEmployee))
                       .Returns(new EmployeeDto
                       {
                           Id = savedEmployee.Id,
                           Name = savedEmployee.Name
                       });

            var handler = new CreateEmployeeCommandHandler(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCode.Created, response.Status);
            Assert.Equal("Employee created successfully", response.Message);

            Assert.Equal(
                new { Id = 10, Name = "John Doe" },
                new { response.Data.Id, response.Data.Name }
            );

            // Verify mappings and repo call
            _mockMapper.Verify(m => m.Map<Employee>(command), Times.Once);
            _mockMapper.Verify(m => m.Map<EmployeeDto>(savedEmployee), Times.Once);
            _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
