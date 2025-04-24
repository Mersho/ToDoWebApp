namespace Application.Commands.DeleteToDoItem;

using MediatR;

public record DeleteToDoItemCommand(int Id) : IRequest<Unit>;
