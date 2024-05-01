using DatapacLibrary.ApplicationCore.Commands;
using FluentValidation;

namespace DatapacLibrary.ApplicationCore.Validators;

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Missing user's name");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("Missing user's password");
    }
}

public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Missing user's Id")
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
            .NotEmpty().WithMessage("User email cannot be empty");
        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.");
    }
}

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(login => login.Password)
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .When(login => !string.IsNullOrEmpty(login.Password));
    }
}


