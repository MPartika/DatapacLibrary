using DatapacLibrary.ApplicationCore.Commands;
using FluentValidation;

namespace DatapacLibrary.ApplicationCore.Validators;

public class AuthenticateAdminCommandValidator : AbstractValidator<AuthenticateAdminCommand>
{
    public AuthenticateAdminCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Missing user's name");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("Missing user's password");
    }
}

public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
{
    public CreateAdminCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("User name cannot be empty");
        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.");
    }
}
