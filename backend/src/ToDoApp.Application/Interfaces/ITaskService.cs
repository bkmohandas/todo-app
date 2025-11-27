using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task<TaskDto?> GetByIdAsync(int id);
    Task<TaskDto> CreateAsync(TaskCreateDto dto);
    Task<TaskDto?> UpdateAsync(int id, TaskUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
