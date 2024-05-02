using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.ApplicationCore.Validators;
using NUnit.Framework;

namespace DatapacLibrary.Test;

public class TestBookValidators
{
    [Test]
    public void IdShouldBeInvalid()
    {
        var deleteBookValidator = new DeleteBookValidator();
        var validationResult = deleteBookValidator.Validate(new DeleteBookCommand {Id = -3});
        Assert.IsNotEmpty(validationResult.Errors);
    }

    [Test]
    public void TitleShouldBeInvalid()
    {
        var createCommand = new CreateBookCommand { Author = "Test", ISBN = "Test", Publisher = "Test", Title = "" };
        var updateCommand = new UpdateBookCommand { Title = ""};
        var exceptionMessage = "Book title cannot be empty";
        var createBookValidator = new CreateBookValidator();
        var updateBookValidator = new UpdateBookValidator();
        
        var createValidationResult = createBookValidator.Validate(createCommand);
        var updateValidationResult = updateBookValidator.Validate(updateCommand);
        
        Assert.IsTrue(createValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage));
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage)); 
    }

    [Test]
    public void PublisherShouldBeInvalid()
    {
        var createCommand = new CreateBookCommand { Author = "Test", ISBN = "Test", Publisher = "", Title = "Test" };
        var updateCommand = new UpdateBookCommand { Publisher = ""};
        var exceptionMessage = "Book publisher cannot be empty";
        var createBookValidator = new CreateBookValidator();
        var updateBookValidator = new UpdateBookValidator();
        
        var createValidationResult = createBookValidator.Validate(createCommand);
        var updateValidationResult = updateBookValidator.Validate(updateCommand);
        
        Assert.IsTrue(createValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage));
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage)); 
    }

    [Test]
    public void ISBNShouldBeInvalid()
    {
        var createCommand = new CreateBookCommand { Author = "Test", ISBN = "", Publisher = "Test", Title = "Test" };
        var updateCommand = new UpdateBookCommand { ISBN = ""};
        var exceptionMessage = "Book ISBN cannot be empty";
        var createBookValidator = new CreateBookValidator();
        var updateBookValidator = new UpdateBookValidator();
        
        var createValidationResult = createBookValidator.Validate(createCommand);
        var updateValidationResult = updateBookValidator.Validate(updateCommand);
        
        Assert.IsTrue(createValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage));
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage)); 
    }

    [Test]
    public void AuthorShouldBeInvalid()
    {
        var createCommand = new CreateBookCommand { Author = "", ISBN = "Test", Publisher = "Test", Title = "Test" };
        var updateCommand = new UpdateBookCommand { Author = ""};
        var exceptionMessage = "Book author cannot be empty";
        var createBookValidator = new CreateBookValidator();
        var updateBookValidator = new UpdateBookValidator();
        
        var createValidationResult = createBookValidator.Validate(createCommand);
        var updateValidationResult = updateBookValidator.Validate(updateCommand);
        
        Assert.IsTrue(createValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage));
        Assert.IsTrue(updateValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage)); 
    }
}