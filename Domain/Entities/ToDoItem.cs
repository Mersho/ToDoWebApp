namespace Domain.Entities;

public class ToDoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ToDoStatus Status { get; set; } = ToDoStatus.Pending;
}
