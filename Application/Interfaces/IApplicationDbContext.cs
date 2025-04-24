namespace Application.Interfaces;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public interface IApplicationDbContext
{
    DbSet<ToDoItem> ToDoItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
