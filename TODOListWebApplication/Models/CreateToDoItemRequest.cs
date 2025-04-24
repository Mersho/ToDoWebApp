namespace TODOListWebApplication.Models;

public class CreateToDoItemRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
