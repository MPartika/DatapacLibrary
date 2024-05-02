using DatapacLibrary.ApplicationCore.Commands;
using FluentValidation;

namespace DatapacLibrary.ApplicationCore.Validators;

public class CreateBookValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Book title cannot be empty");
        RuleFor(command => command.Publisher)
            .NotEmpty().WithMessage("Book publisher cannot be empty");
        RuleFor(command => command.ISBN)
            .NotEmpty().WithMessage("Book ISBN cannot be empty");
        RuleFor(command => command.Author)
            .NotEmpty().WithMessage("Book author cannot be empty");
    }
}

public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Book title cannot be empty")
            .When(command => command.Title != null);
        RuleFor(command => command.Publisher)
            .NotEmpty().WithMessage("Book publisher cannot be empty")
            .When(command => command.Publisher != null);
        RuleFor(command => command.ISBN)
            .NotEmpty().WithMessage("Book ISBN cannot be empty")
            .When(command => command.ISBN != null);
        RuleFor(command => command.Author)
            .NotEmpty().WithMessage("Book author cannot be empty")
            .When(command => command.Author != null);
    }
}

public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Book id cannot be empty");
    }
}

