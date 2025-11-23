using AutoMapper;
using EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories;
using EMPLOYEE_MANAGEMENT.Application.Dto;
using EMPLOYEE_MANAGEMENT.Application.Features.Roles.Query;
using EMPLOYEE_MANAGEMENT.Application.Wrapper;
using EMPLOYEE_MANAGEMENT.Application.logging;
using EMPLOYEE_MANAGEMENT.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EMPLOYEE_MANAGEMENT.Tests.Application.Features.Roles.Query
{
    public class GetAllRolesQueryHandlerTests
    {
        private readonly Mock<IRoleRepository> _mockRepo;
        private readonly Mock<IAppLogger<GetAllRolesQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetAllRolesQueryHandlerTests()
        {
            _mockRepo = new Mock<IRoleRepository>();
            _mockLogger = new Mock<IAppLogger<GetAllRolesQueryHandler>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, RoleDto>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldReturnAllRoles()
        {
            // Arrange
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin", Description = "Admin role" },
                new Role { Id = 2, Name = "User", Description = "User role" }
            };

            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync(roles);

            var handler = new GetAllRolesQueryHandler(_mockRepo.Object, _mapper, _mockLogger.Object);
            var query = new GetAllRolesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Admin", result.Data[0].Name);
            Assert.Equal("User", result.Data[1].Name);

            _mockRepo.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockLogger.VerifyNoOtherCalls();
        }
    }
}
