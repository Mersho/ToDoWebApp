namespace Application.Queries.GetToDoItemById;

using MediatR;
using Application.DTOs;
using Application.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

public class GetToDoItemByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetToDoItemByIdQuery, ToDoItemDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ToDoItemDto> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoItems.FindAsync([request.Id], cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(ToDoItem), request.Id);

        return new ToDoItemDto(entity.Id, entity.Title, entity.Description, entity.Status);
    }
}
