namespace Application.UnitTests;

using Xunit;
using Infrastructure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using Application.Commands.CreateToDoItem;
using Application.Commands.UpdateToDoItem;
using Application.Commands.DeleteToDoItem;
using Application.Queries.GetToDoItemById;
using Application.Queries.GetAllToDoItems;

public class ToDoHandlerTests
{
    private ApplicationDbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async void CreateHandler_CreatesEntity()
    {
        using var context = GetContext();
        var handler = new CreateToDoItemCommandHandler(context);
        var id = await handler.Handle(new CreateToDoItemCommand("title", "desc"), CancellationToken.None);
        Assert.True(id > 0);
        var entity = await context.ToDoItems.FindAsync(id);
        Assert.NotNull(entity);
        Assert.Equal("title", entity.Title);
    }

    [Fact]
    public async void UpdateHandler_UpdatesEntity()
    {
        using var context = GetContext();
        var entity = new ToDoItem { Title = "t", Description = "d", Status = ToDoStatus.Pending };
        context.ToDoItems.Add(entity);
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new UpdateToDoItemCommandHandler(context);
        await handler.Handle(new UpdateToDoItemCommand(entity.Id, "new", "newdesc", ToDoStatus.Completed), CancellationToken.None);
        var updated = await context.ToDoItems.FindAsync(entity.Id);

        Assert.NotNull(updated);
        Assert.Equal("new", updated.Title);
        Assert.Equal(ToDoStatus.Completed, updated.Status);
    }

    [Fact]
    public async void DeleteHandler_DeletesEntity()
    {
        using var context = GetContext();
        var entity = new ToDoItem { Title = "t", Description = "d", Status = ToDoStatus.Pending };
        context.ToDoItems.Add(entity);
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new DeleteToDoItemCommandHandler(context);
        await handler.Handle(new DeleteToDoItemCommand(entity.Id), CancellationToken.None);
        var deleted = await context.ToDoItems.FindAsync(entity.Id);

        Assert.Null(deleted);
    }

    [Fact]
    public async void GetByIdQueryHandler_ReturnsDto()
    {
        using var context = GetContext();
        var entity = new ToDoItem { Title = "t", Description = "d", Status = ToDoStatus.Pending };
        context.ToDoItems.Add(entity);
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new GetToDoItemByIdQueryHandler(context);
        var dto = await handler.Handle(new GetToDoItemByIdQuery(entity.Id), CancellationToken.None);

        Assert.Equal(entity.Title, dto.Title);
        Assert.Equal(entity.Status, dto.Status);
    }

    [Fact]
    public async void GetAllQueryHandler_ReturnsList()
    {
        using var context = GetContext();
        context.ToDoItems.Add(new ToDoItem { Title = "t", Description = "d", Status = ToDoStatus.Pending });
        await context.SaveChangesAsync(CancellationToken.None);

        var handler = new GetAllToDoItemsQueryHandler(context);
        var list = await handler.Handle(new GetAllToDoItemsQuery(), CancellationToken.None);

        Assert.Single(list);
    }
}
