namespace Application.Commands.UpdateToDoItem;

using MediatR;
using Domain.Entities;

public record UpdateToDoItemCommand(int Id, string Title, string Description, ToDoStatus Status) : IRequest<Unit>;
