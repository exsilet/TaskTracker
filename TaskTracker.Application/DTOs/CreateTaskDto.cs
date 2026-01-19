using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Application.DTOs;

public class CreateTaskDto
{
    [Required(ErrorMessage = "Название задачи обязательно")]
    [StringLength(200, MinimumLength = 3, 
        ErrorMessage = "Название должно быть от 3 до 200 символов")]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов")]
    public string? Description { get; set; }
    
    [FutureDate(ErrorMessage = "Дата выполнения должна быть в будущем")]
    public DateTime? DueDate { get; set; }
    
    [Range(1, 4, ErrorMessage = "Приоритет должен быть от 1 до 4")]
    public int Priority { get; set; } = 2;
}

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime date)
        {
            return date > DateTime.Now;
        }
        return true;
    }
}