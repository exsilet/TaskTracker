using TaskTracker.Application.DTOs;

namespace TaskTracker.Application.Services;

public interface ITaskService
{
    Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
    Task<List<TaskDto>> GetAllTasksAsync();
    Task<TaskDto?> GetTaskByIdAsync(int id);
    Task UpdateTaskAsync(int id, CreateTaskDto updateTaskDto);
    Task DeleteTaskAsync(int id);
}