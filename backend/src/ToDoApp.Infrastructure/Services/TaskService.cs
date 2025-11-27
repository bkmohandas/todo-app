using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Infrastructure.Data;
using ToDoApp.Infrastructure.Mapping;

namespace ToDoApp.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;
    private readonly ILogger<TaskService> _logger;

    public TaskService(AppDbContext db, ILogger<TaskService> logger)
    {
        _db = db;
        _logger = logger;
    }
    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        var entities = await _db.Tasks.AsNoTracking()
            .OrderByDescending(t => t.CreatedAtUtc)
            .ToListAsync();

        return entities.Select(TaskMapping.ToDto);
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var entity = await _db.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (entity == null)
        {
            _logger.LogWarning("Task with ID {TaskId} not found", id);
        }
        else
        {
            _logger.LogInformation("Task with ID {TaskId} retrieved", id);
        }

        return entity is null ? null : TaskMapping.ToDto(entity);
    }

    public async Task<TaskDto> CreateAsync(TaskCreateDto dto)
    {
        var entity = TaskMapping.FromCreate(dto);
        
        await _db.Tasks.AddAsync(entity);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Task created with ID {TaskId} and Title {Title}", entity.Id, entity.Title);

        return TaskMapping.ToDto(entity);
    }

    public async Task<TaskDto?> UpdateAsync(int id, TaskUpdateDto dto)
    {
        var entity = await _db.Tasks.FindAsync(id);
        if (entity is null)
        {
            _logger.LogWarning("Task with ID {TaskId} not found", id);
            return null;
        }

        TaskMapping.ApplyUpdate(entity, dto);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Task updated with ID {TaskId} and Title {Title}", entity.Id, entity.Title);

        return TaskMapping.ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Tasks.FindAsync(id);
        if (entity is null)
        {
            _logger.LogWarning("Attempted to delete non-existent task with ID {TaskId}", id);
            return false;
        }
        
        _db.Tasks.Remove(entity);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Task with ID {TaskId} deleted", id);
        return true;
    }
}
