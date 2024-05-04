using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.ApplicationCore.Validators;

namespace DatapacLibrary.Test;

public class TestAdminValidators
{
    [Test]
    public void ShouldBeValidPassword()
    {
        var password = "1234abcdEFGH";
        var createValidator = new CreateAdminCommandValidator();
        var createCommand = new CreateAdminCommand { Name = "Test", Password = password };
        var createValidationResult = createValidator.Validate(createCommand);
        Assert.That(createValidationResult.Errors, Is.Empty);
    }

    [Test]
    public void PasswordShouldBeTooShort()
    {
        var password = "1234";
        var createValidator = new CreateAdminCommandValidator();
        var createCommand = new CreateAdminCommand { Name = "Test", Password = password };
        var createValidationResult = createValidator.Validate(createCommand);
        Assert.That(createValidationResult.Errors, Is.Not.Empty);
        Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password length must be at least 8."), Is.True);
    }

    [Test]
    public void PasswordShouldBeMissingLowerCaseLetters()
    {
        var password = "1234EFGH";
        var createValidator = new CreateAdminCommandValidator();
        var createCommand = new CreateAdminCommand { Name = "Test", Password = password };
        var createValidationResult = createValidator.Validate(createCommand);
        Assert.That(createValidationResult.Errors, Is.Not.Empty);
        Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one lowercase letter."), Is.True);
    }

    [Test]
    public void PasswordShouldBeMissingUpperCaseLetters()
    {
        var password = "1234abcd";
        var createValidator = new CreateAdminCommandValidator();
        var createCommand = new CreateAdminCommand { Name = "Test", Password = password };
        var createValidationResult = createValidator.Validate(createCommand);
        Assert.That(createValidationResult.Errors, Is.Not.Empty);
        Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one uppercase letter."), Is.True);
    }
}