using DatapacLibrary.ApplicationCore.Commands;
using FluentValidation;

namespace DatapacLibrary.ApplicationCore.Validators;



public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Missing user's Id");
    }
}

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("User name cannot be empty");
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("User email cannot be empty")
            .EmailAddress().WithMessage("User email must be valid");
    }
}

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("User name cannot be empty")
            .When(command => command.Name != null);
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("User email cannot be empty")
            .EmailAddress().WithMessage("User email must be valid")
            .When(command => !string.IsNullOrEmpty(command.Email));
    }
}


