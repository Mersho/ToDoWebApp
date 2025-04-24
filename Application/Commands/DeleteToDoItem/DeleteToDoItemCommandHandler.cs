namespace Application.Commands.DeleteToDoItem;

using MediatR;
using Application.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteToDoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoItems.FindAsync([request.Id], cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(ToDoItem), request.Id);

        _context.ToDoItems.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
