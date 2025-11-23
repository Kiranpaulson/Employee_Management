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
using EMPLOYEE_MANAGEMENT.Application.logging;

public class CreateDepartmentCommandHandlerTests
{
    private readonly Mock<IDepartmentRepository> _mockRepo;
    private readonly Mock<IAppLogger<CreateDepartmentCommandHandler>> _mockLogger;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandlerTests()
    {
        _mockRepo = new Mock<IDepartmentRepository>();
        _mockLogger = new Mock<IAppLogger<CreateDepartmentCommandHandler>>();

        // AutoMapper config for CreateDepartmentCommand → Department → DepartmentDto
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateDepartmentCommand, Department>();
            cfg.CreateMap<Department, DepartmentDto>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task Handle_ShouldCreateDepartment_AndReturnDto()
    {
        // Arrange
        var command = new CreateDepartmentCommand
        {
            Name = "IT",
            Description = "Tech Department"
        };

        var savedEntity = new Department
        {
            Id = 10,
            Name = "IT",
            Description = "Tech Department",
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(savedEntity);

        var handler = new CreateDepartmentCommandHandler(_mockRepo.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert all properties in a single step using an anonymous object
        Assert.Equal(new { Id = 10, Name = "IT", Description = "Tech Department" },
                     new { result.Data.Id, result.Data.Name, result.Data.Description });

        // Ensure CreateAsync was called once with any Department and any CancellationToken
        _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()), Times.Once);

        // Optional: verify logger was called at least once
        _mockLogger.Verify(l => l.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()), Times.AtLeastOnce);
    }
}
