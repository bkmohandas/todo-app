using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using ToDoApp.Application.DTOs;
using ToDoApp.Infrastructure.Data;
using ToDoApp.Infrastructure.Services;

namespace ToDoApp.Tests.Infrastructure;

public class TaskServiceTests
{
    [Fact]
    public async Task Create_And_GetAll_Should_Work()
    {
        // Arrange: use in-memory EF Core database
        var opts = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var db = new AppDbContext(opts);
        
        // Inject a NullLogger (no-op) for testing
        var logger = NullLogger<TaskService>.Instance;

        var svc = new TaskService(db, logger);

        // Act: create a task
        var created = await svc.CreateAsync(new TaskCreateDto("Test", null, null, 1));

        // Assert: task was created with an Id
        created.Id.Should().BeGreaterThan(0);

        // Act: retrieve all tasks
        var list = await svc.GetAllAsync();

        // Assert: list contains the created task
        list.Should().ContainSingle().Which.Title.Should().Be("Test");
    }
}
