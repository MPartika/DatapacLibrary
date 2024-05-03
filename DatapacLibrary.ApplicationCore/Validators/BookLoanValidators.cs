using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using FluentValidation;

namespace DatapacLibrary.ApplicationCore.Validators
{
    public class CreateLoanValidators : AbstractValidator<CreateNewLoanCommand>
    {
        private readonly IBookLoanRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        public CreateLoanValidators(IBookLoanRepository repository, IBookRepository bookRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;

            RuleFor(x => x.BookId)
                .MustAsync(async (value, cancellation) => await UserExists(value)).WithMessage("User is not Valid");

            RuleFor(x => x.BookId)
                .MustAsync(async (value, cancellation) => await BookExists(value)).WithMessage("Book is not valid")
                .MustAsync(async (value, cancellation) => await _repository.IsBookAvailable(value)).WithMessage("Book is not available");
        }

        private async Task<bool> BookExists(long id) => (await _bookRepository.GetBookAsync(id)) != null;
        private async Task<bool> UserExists(long id) => (await _userRepository.GetUserAsync(id)) != null;

    }
}
