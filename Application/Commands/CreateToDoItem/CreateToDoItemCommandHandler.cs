namespace Application.Commands.CreateToDoItem;

using MediatR;
using Application.Interfaces;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateToDoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDoItem
        {
            Title = request.Title,
            Description = request.Description,
            Status = ToDoStatus.Pending
        };

        _context.ToDoItems.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
