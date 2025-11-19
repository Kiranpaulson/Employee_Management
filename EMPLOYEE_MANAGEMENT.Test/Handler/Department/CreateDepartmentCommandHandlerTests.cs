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

public class CreateDepartmentCommandHandlerTests
{
    private readonly Mock<IDepartmentRepository> _mockRepo;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandlerTests()
    {
        _mockRepo = new Mock<IDepartmentRepository>();

        // AutoMapper config for Department → DepartmentDto
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

        // Mock repo CreateAsync
        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Department>()))
                 .ReturnsAsync(savedEntity);

        var handler = new CreateDepartmentCommandHandler(_mockRepo.Object, _mapper);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert (NO SUCCESS CHECKS)
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(10, result.Data.Id);
        Assert.Equal("IT", result.Data.Name);
        Assert.Equal("Tech Department", result.Data.Description);

        // Ensure CreateAsync was called once
        _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Department>()), Times.Once);
    }
}
