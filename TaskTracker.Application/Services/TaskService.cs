using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.DTOs;
using TaskTracker.Application.Exceptions;
using TaskTracker.Domain.Entities;
using TaskTracker.Infrastructure.Data;

namespace TaskTracker.Application.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        var taskItem = new TaskItem
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            DueDate = createTaskDto.DueDate,
            Priority = (TaskPriority)createTaskDto.Priority,
            CreatedAt = DateTime.UtcNow,
            IsCompleted = false
        };
        
        _context.Tasks.Add(taskItem);
        await _context.SaveChangesAsync();
        
        return MapToDto(taskItem);
    }

    public async Task<List<TaskDto>> GetAllTasksAsync()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return tasks.Select(MapToDto).ToList();
    }

    public async Task<TaskDto?> GetTaskByIdAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        return task == null ? null : MapToDto(task);
    }

    public async Task UpdateTaskAsync(int id, CreateTaskDto updateTaskDto)
    {
        var existingTask = await _context.Tasks.FindAsync(id);
        if (existingTask == null)
            throw new NotFoundException(nameof(TaskItem), id);

        existingTask.Title = updateTaskDto.Title;
        existingTask.Description = updateTaskDto.Description;
        existingTask.DueDate = updateTaskDto.DueDate;
        existingTask.Priority = (TaskPriority)updateTaskDto.Priority;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            throw new NotFoundException(nameof(TaskItem), id);

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
    
    private TaskDto MapToDto(TaskItem task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedAt = task.CreatedAt,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted,
            Priority = (int)task.Priority
        };
    }
}