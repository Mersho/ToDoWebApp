namespace Application.UnitTests;

using Xunit;
using Application.Commands.CreateToDoItem;
using Application.Commands.UpdateToDoItem;
using FluentValidation;
using Domain.Entities;

public class ValidationTests
{
    [Fact]
    public void CreateCommandValidator_FailsWhenTitleEmpty()
    {
        var validator = new CreateToDoItemCommandValidator();
        var result = validator.Validate(new CreateToDoItemCommand("", "desc"));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void UpdateCommandValidator_FailsWhenIdInvalid()
    {
        var validator = new UpdateToDoItemCommandValidator();
        var result = validator.Validate(new UpdateToDoItemCommand(0, "", "", ToDoStatus.Pending));
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Id");
    }
}
