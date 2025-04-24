namespace Application.Commands.CreateToDoItem;

using FluentValidation;

public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
        RuleFor(x => x.Description)
            .MaximumLength(1000);
    }
}
