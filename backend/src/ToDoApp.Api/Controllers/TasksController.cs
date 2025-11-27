using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Infrastructure.Data;
using ToDoApp.Infrastructure.Services;

namespace ToDoApp.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    public TasksController(ITaskService service)
    {
        _taskService = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll()
    {
        var items = await _taskService.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskDto>> GetById(int id)
    {
        var item = await _taskService.GetByIdAsync(id);
        return item is null ? NotFound(new ErrorResponse("NOT_FOUND", "Task not found.")) : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create([FromBody] TaskCreateDto dto)
    {
        var created = await _taskService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TaskDto>> Update(int id, [FromBody] TaskUpdateDto dto)
    {
        var updated = await _taskService.UpdateAsync(id, dto);
        return updated is null
            ? NotFound(new ErrorResponse("NOT_FOUND", "Task not found."))
            : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _taskService.DeleteAsync(id);

        return deleted
            ? NoContent()
            : NotFound(new ErrorResponse("NOT_FOUND", "Task not found."));
    }


}
