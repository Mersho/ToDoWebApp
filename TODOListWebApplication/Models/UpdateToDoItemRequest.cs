namespace TODOListWebApplication.Models;

using Domain.Entities;

public class UpdateToDoItemRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ToDoStatus Status { get; set; }
}
