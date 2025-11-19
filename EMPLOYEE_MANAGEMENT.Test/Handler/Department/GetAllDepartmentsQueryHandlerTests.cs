using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetAllDepartmentsQueryHandlerTests
{
    private readonly Mock<IDepartmentRepository> _mockRepo;
    private readonly IMapper _mapper;

    public GetAllDepartmentsQueryHandlerTests()
    {
        _mockRepo = new Mock<IDepartmentRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Department, DepartmentDto>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoDepartmentsExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(new List<Department>());

        var handler = new GetAllDepartmentsQueryHandler(_mockRepo.Object, _mapper);

        // Act
        var result = await handler.Handle(new GetAllDepartmentsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedDepartments_WhenDepartmentsExist()
    {
        // Arrange
        var departments = new List<Department>
        {
            new Department { Id = 1, Name = "HR", Description = "HR Dept" },
            new Department { Id = 2, Name = "IT", Description = "IT Dept" }
        };

        _mockRepo.Setup(r => r.GetAllAsync())
                 .ReturnsAsync(departments);

        var handler = new GetAllDepartmentsQueryHandler(_mockRepo.Object, _mapper);

        // Act
        var result = await handler.Handle(new GetAllDepartmentsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);
        Assert.Equal("HR", result.Data[0].Name);
        Assert.Equal("IT", result.Data[1].Name);
    }
}
