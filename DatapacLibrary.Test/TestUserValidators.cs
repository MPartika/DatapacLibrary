using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.ApplicationCore.Validators;

namespace DatapacLibrary.Test;

public class TestUserValidators
{
    

    [Test]
    public void EmailShouldBeInvalid()
    {
        var createValidator = new CreateUserValidator();
        var updateValidator = new UpdateUserValidator();
        var createCommand = new CreateUserCommand { Name= "Test", Email="Test"};
        var updateCommand = new UpdateUserCommand { Email="Test" };
        var createValidationResult = createValidator.Validate(createCommand);
        var updateValidationResult = updateValidator.Validate(updateCommand);
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == "User email must be valid"), Is.True);
            Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == "User email must be valid"), Is.True);
        });
    }

    [Test]
    public void NameShouldBeInvalid()
    {
        var updateValidator = new UpdateUserValidator();
        var updateCommand = new UpdateUserCommand { Name="" };
        var updateValidationResult = updateValidator.Validate(updateCommand);
        Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == "User name cannot be empty"), Is.True);
    }
}