namespace Application.Queries.GetAllToDoItems;

using MediatR;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetAllToDoItemsQueryHandler : IRequestHandler<GetAllToDoItemsQuery, List<ToDoItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllToDoItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ToDoItemDto>> Handle(GetAllToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ToDoItems.ToListAsync(cancellationToken);
        return entities.Select(e => new ToDoItemDto(e.Id, e.Title, e.Description, e.Status)).ToList();
    }
}
