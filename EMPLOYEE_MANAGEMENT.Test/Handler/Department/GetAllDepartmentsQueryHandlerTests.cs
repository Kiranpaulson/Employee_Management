using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Handler;
using EMPLOYEE_MANAGEMENT.Application.Features.Departments.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.Abstractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.CustomException; // For NotFoundException
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetAllDepartmentsQueryHandlerTests
{
    private readonly Mock<IDepartmentRepository> _mockRepo;
    private readonly Mock<IAppLogger<GetAllDepartmentsQueryHandler>> _mockLogger;
    private readonly IMapper _mapper;

    public GetAllDepartmentsQueryHandlerTests()
    {
        _mockRepo = new Mock<IDepartmentRepository>();
        _mockLogger = new Mock<IAppLogger<GetAllDepartmentsQueryHandler>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Department, DepartmentDto>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenNoDepartmentsExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new List<Department>());

        var handler = new GetAllDepartmentsQueryHandler(_mockRepo.Object, _mapper, _mockLogger.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            handler.Handle(new GetAllDepartmentsQuery(), CancellationToken.None));
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

        _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(departments);

        var handler = new GetAllDepartmentsQueryHandler(_mockRepo.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllDepartmentsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Count);

        // Single-step equivalence check using anonymous objects
        Assert.Equal(
            new[]
            {
                new { Id = 1, Name = "HR", Description = "HR Dept" },
                new { Id = 2, Name = "IT", Description = "IT Dept" }
            },
            new[]
            {
                new { result.Data[0].Id, result.Data[0].Name, result.Data[0].Description },
                new { result.Data[1].Id, result.Data[1].Name, result.Data[1].Description }
            }
        );
    }
}
