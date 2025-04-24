using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands.CreateToDoItem;
using Application.Commands.UpdateToDoItem;
using Application.Commands.DeleteToDoItem;
using Application.Queries.GetToDoItemById;
using Application.Queries.GetAllToDoItems;
using Application.DTOs;
using TODOListWebApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODOListWebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToDoItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateToDoItemRequest request)
    {
        var id = await _mediator.Send(new CreateToDoItemCommand(request.Title, request.Description));
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet]
    public async Task<ActionResult<List<ToDoItemDto>>> GetAll()
    {
        var items = await _mediator.Send(new GetAllToDoItemsQuery());
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemDto>> GetById(int id)
    {
        var item = await _mediator.Send(new GetToDoItemByIdQuery(id));
        return Ok(item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateToDoItemRequest request)
    {
        await _mediator.Send(new UpdateToDoItemCommand(id, request.Title, request.Description, request.Status));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteToDoItemCommand(id));
        return NoContent();
    }
}
