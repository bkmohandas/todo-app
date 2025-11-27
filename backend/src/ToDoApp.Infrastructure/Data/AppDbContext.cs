using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Application.Validation;

namespace ToDoApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<TaskItem>(e =>
        {
            e.Property(x => x.Title).IsRequired().HasMaxLength(ValidationConstants.TitleMaxLength);
            e.Property(x => x.Description).HasMaxLength(ValidationConstants.DescriptionMaxLength);
            e.Property(x => x.Priority).HasDefaultValue(ValidationConstants.PriorityMin);
            e.Property(x => x.CreatedAtUtc).HasConversion<DateTime>();
        });
    }
}
