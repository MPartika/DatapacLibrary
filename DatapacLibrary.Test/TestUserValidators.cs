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
        Assert.IsEmpty(createValidationResult.Errors);
        Assert.IsEmpty(updateValidationResult.Errors);
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
        Assert.IsNotEmpty(createValidationResult.Errors);
        Assert.IsNotEmpty(updateValidationResult.Errors);
        Assert.IsTrue(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password length must be at least 8."));
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == "Your password length must be at least 8."));
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
        Assert.IsNotEmpty(createValidationResult.Errors);
        Assert.IsNotEmpty(updateValidationResult.Errors);
        Assert.IsTrue(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one lowercase letter."));
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one lowercase letter."));
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
        Assert.IsNotEmpty(createValidationResult.Errors);
        Assert.IsNotEmpty(updateValidationResult.Errors);
        Assert.IsTrue(createValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one uppercase letter."));
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == "Your password must contain at least one uppercase letter."));
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
        Assert.IsTrue(createValidationResult.Errors.Any(x => x.ErrorMessage == "User email must be valid"));
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == "User email must be valid"));
    }

    [Test]
    public void NameShouldBeInvalid()
    {
        var updateValidator = new UpdateUserValidator();
        var updateCommand = new UpdateUserCommand { Name="" };
        var updateValidationResult = updateValidator.Validate(updateCommand);
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == "User name cannot be empty"));
    }
}