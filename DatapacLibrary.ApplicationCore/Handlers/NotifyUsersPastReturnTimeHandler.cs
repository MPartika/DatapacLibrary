using DatapacLibrary.ApplicationCore.Commands;
using DatapacLibrary.Domain.Contracts;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Handlers;

public class NotifyUsersPastReturnTimeHandler : IRequestHandler<NotifyUsersPastReturnTimeCommand>
{
    private readonly IBookLoanRepository _repository;
    private readonly IEmailService _service;

    public NotifyUsersPastReturnTimeHandler(IBookLoanRepository repository, IEmailService service)
    {
        _repository = repository;
        _service = service;
    }

    public async Task Handle(NotifyUsersPastReturnTimeCommand request, CancellationToken cancellationToken)
    {
        var notReturnedLoans = (await _repository.GetLoansPastReturnTimeAsync()).ToList();

        notReturnedLoans.ForEach(async x =>
        {
            await _service.SendEmailNotificationToUser(x.Name, x.Email, x.Title, x.ValidUntil);
            await _repository.ExtendValidUnitByDays(x.LoanId, 2);
        });
    }
}