namespace Application.Commands.UpdateToDoItem;

using MediatR;
using Application.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateToDoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoItems.FindAsync([request.Id], cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(ToDoItem), request.Id);

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Status = request.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
