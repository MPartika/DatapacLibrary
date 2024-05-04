using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.ApplicationCore.Validators;

namespace DatapacLibrary.Test;

public class TestBookValidators
{
    [Test]
    public void IdShouldBeInvalid()
    {
        var deleteBookValidator = new DeleteBookValidator();
        var validationResult = deleteBookValidator.Validate(new DeleteBookCommand {Id = -3});
        Assert.That(validationResult.Errors, Is.Not.Empty);
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
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage), Is.True);
            Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage), Is.True);
        });
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
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage), Is.True);
            Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage), Is.True);
        });
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
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage), Is.True);
            Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage), Is.True);
        });
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
        Assert.Multiple(() =>
        {
            Assert.That(createValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage), Is.True);
            Assert.That(updateValidationResult.Errors.Any(x => x.ErrorMessage == exceptionMessage), Is.True);
        });
    }
}