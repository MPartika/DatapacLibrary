using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class NotifyUsersTimeHandler : IRequestHandler<NotifyUsersCommand>
{
    private readonly IBookLoanRepository _repository;
    private readonly IEmailService _service;

    public NotifyUsersTimeHandler(IBookLoanRepository repository, IEmailService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task Handle(NotifyUsersCommand request, CancellationToken cancellationToken)
    {
        var notReturnedLoans = await _repository.GetLoansPastReturnTimeAsync();

        foreach (var loan in notReturnedLoans)
        {
            await _service.SendEmailNotificationToUser(loan.Name, loan.Email, loan.Title, loan.ValidUntil);
            await _repository.ExtendValidUnitByDays(loan.LoanId, 5);
        };
    }
}