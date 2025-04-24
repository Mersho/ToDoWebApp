namespace API.UnitTests;

using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TODOListWebApplication.Controllers;
using TODOListWebApplication.Models;
using Application.DTOs;
using Application.Queries.GetAllToDoItems;
using Application.Queries.GetToDoItemById;
using Application.Commands.CreateToDoItem;
using Application.Commands.UpdateToDoItem;
using Application.Commands.DeleteToDoItem;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class ToDoItemsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ToDoItemsController _controller;

    public ToDoItemsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ToDoItemsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResultWithItems()
    {
        var items = new List<ToDoItemDto> { new ToDoItemDto(1, "t", "d", ToDoStatus.Pending) };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllToDoItemsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(items);

        var actionResult = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(items, okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsOkResultWithItem()
    {
        var dto = new ToDoItemDto(1, "t", "d", ToDoStatus.Pending);
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetToDoItemByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(dto);

        var actionResult = await _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(dto, okResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateToDoItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(5);

        var request = new CreateToDoItemRequest { Title = "t", Description = "d" };
        var actionResult = await _controller.Create(request);

        var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        Assert.Equal(5, createdResult.Value);
        Assert.Equal(nameof(ToDoItemsController.GetById), createdResult.ActionName);
    }

    [Fact]
    public async Task Update_ReturnsNoContent()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateToDoItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);

        var request = new UpdateToDoItemRequest { Title = "t", Description = "d", Status = ToDoStatus.Completed };
        var result = await _controller.Update(1, request);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteToDoItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);

        var result = await _controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }
}
