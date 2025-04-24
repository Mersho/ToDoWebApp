namespace Application.Queries.GetAllToDoItems;

using MediatR;
using Application.DTOs;
using System.Collections.Generic;

public record GetAllToDoItemsQuery() : IRequest<List<ToDoItemDto>>;
