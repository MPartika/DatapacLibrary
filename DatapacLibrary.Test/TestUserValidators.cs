using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.ApplicationCore.Validators;

namespace DatapacLibrary.Test;

public class TestUserValidators
{
    [Test]
    public void ShouldBeValidPassword()
    {
        var password = "1234abcdEFGH";
        var createValidator = new CreateUserValidator();
        var updateValidator = new UpdateUserValidator();
        var createCommand = new CreateUserCommand { Name= "Test", Email="Test@example.com", Password =  password};
        var updateCommand = new UpdateUserCommand { Password =  password};
        var createValidationResult = createValidator.Validate(createCommand);
        var updateValidationResult = updateValidator.Validate(updateCommand);
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors, Is.Empty);
            Assert.That(updateValidationResult.Errors, Is.Empty);
        });
    }

    [Test]
    public void PasswordShouldBeTooShort()
    {
        var password = "1234";
        var createValidator = new CreateUserValidator();
        var updateValidator = new UpdateUserValidator();
        var createCommand = new CreateUserCommand { Name= "Test", Email="Test@example.com", Password =  password};
        var updateCommand = new UpdateUserCommand { Password =  password};
        var createValidationResult = createValidator.Validate(createCommand);
        var updateValidationResult = updateValidator.Validate(updateCommand);
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors, Is.Not.Empty);
            Assert.That(updateValidationResult.Errors, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password length must be at least 8."), Is.True);
            Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == "Your password length must be at least 8."), Is.True);
        });
    }

    [Test]
    public void PasswordShouldBeMissingLowerCaseLetters()
    {
        var password = "1234EFGH";
        var createValidator = new CreateUserValidator();
        var updateValidator = new UpdateUserValidator();
        var createCommand = new CreateUserCommand { Name= "Test", Email="Test@example.com", Password =  password};
        var updateCommand = new UpdateUserCommand { Password =  password};
        var createValidationResult = createValidator.Validate(createCommand);
        var updateValidationResult = updateValidator.Validate(updateCommand);
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors, Is.Not.Empty);
            Assert.That(updateValidationResult.Errors, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one lowercase letter."), Is.True);
            Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one lowercase letter."), Is.True);
        });
    }

    [Test]
    public void PasswordShouldBeMissingUpperCaseLetters()
    {
        var password = "1234abcd";
        var createValidator = new CreateUserValidator();
        var updateValidator = new UpdateUserValidator();
        var createCommand = new CreateUserCommand { Name= "Test", Email="Test@example.com", Password =  password};
        var updateCommand = new UpdateUserCommand { Password =  password};
        var createValidationResult = createValidator.Validate(createCommand);
        var updateValidationResult = updateValidator.Validate(updateCommand);
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors, Is.Not.Empty);
            Assert.That(updateValidationResult.Errors, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one uppercase letter."), Is.True);
            Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one uppercase letter."), Is.True);
        });
    }

    [Test]
    public void EmailShouldBeInvalid()
    {
        var password = "1234abcdEFGH";
        var createValidator = new CreateUserValidator();
        var updateValidator = new UpdateUserValidator();
        var createCommand = new CreateUserCommand { Name= "Test", Email="Test", Password =  password};
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