namespace Application.Queries.GetToDoItemById;

using MediatR;
using Application.DTOs;

public record GetToDoItemByIdQuery(int Id) : IRequest<ToDoItemDto>;
