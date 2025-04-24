namespace Application.Commands.CreateToDoItem;

using MediatR;

public record CreateToDoItemCommand(string Title, string Description) : IRequest<int>;
