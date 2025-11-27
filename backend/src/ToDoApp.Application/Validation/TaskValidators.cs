using FluentValidation;
using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Validation;

public class TaskCreateValidator : AbstractValidator<TaskCreateDto>
{
    public TaskCreateValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(ValidationConstants.TitleMaxLength);
        RuleFor(x => x.Description).MaximumLength(ValidationConstants.DescriptionMaxLength);
        RuleFor(x => x.Priority).InclusiveBetween(ValidationConstants.PriorityMin, ValidationConstants.PriorityMax);
        RuleFor(x => x.DueDateUtc).GreaterThan(DateTime.UtcNow).When(x => x.DueDateUtc.HasValue);
    }
}

public class TaskUpdateValidator : AbstractValidator<TaskUpdateDto>
{
    public TaskUpdateValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(ValidationConstants.TitleMaxLength);
        RuleFor(x => x.Description).MaximumLength(ValidationConstants.DescriptionMaxLength);
        RuleFor(x => x.Priority).InclusiveBetween(ValidationConstants.PriorityMin, ValidationConstants.PriorityMax);
        RuleFor(x => x.DueDateUtc).GreaterThan(DateTime.UtcNow).When(x => x.DueDateUtc.HasValue && x.IsCompleted == false);
    }
}
