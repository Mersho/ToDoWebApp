namespace Application.DTOs;

using Domain.Entities;

public record ToDoItemDto(int Id, string Title, string Description, ToDoStatus Status);
