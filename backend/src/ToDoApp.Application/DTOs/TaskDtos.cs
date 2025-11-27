namespace ToDoApp.Application.DTOs;

public record TaskCreateDto(string Title, string? Description, DateTime? DueDateUtc, int Priority);
public record TaskUpdateDto(string Title, string? Description, bool IsCompleted, DateTime? DueDateUtc, int Priority);
public record TaskDto(int Id, string Title, string? Description, bool IsCompleted, DateTime CreatedAtUtc, DateTime? DueDateUtc, int Priority);
public record ErrorResponse(string Code, string Message);