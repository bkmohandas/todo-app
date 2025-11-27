using ToDoApp.Application.DTOs;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Mapping;

public static class TaskMapping
{
    public static TaskDto ToDto(TaskItem x)
    {
        return new TaskDto(
            x.Id,
            x.Title,
            x.Description,
            x.IsCompleted,
            DateTime.SpecifyKind(x.CreatedAtUtc, DateTimeKind.Utc),
            x.DueDateUtc.HasValue ? DateTime.SpecifyKind(x.DueDateUtc.Value, DateTimeKind.Utc) : null,
            x.Priority
        );
    }

    public static TaskItem FromCreate(TaskCreateDto dto)
    {
        return new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDateUtc = dto.DueDateUtc,
            Priority = dto.Priority
        };
    }

    public static void ApplyUpdate(TaskItem entity, TaskUpdateDto dto)
    {
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.IsCompleted = dto.IsCompleted;
        entity.DueDateUtc = dto.DueDateUtc;
        entity.Priority = dto.Priority;
    }
}
