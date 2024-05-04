using DatapacLibrary.ApplicationCore.Commands;
using MediatR;

namespace DatapacLibrary.Web.Jobs;

internal class NotifyUsersJob
{
    private readonly IMediator _mediat;

    public NotifyUsersJob(IMediator mediat)
    {
        _mediat = mediat;
    }

    public async Task SendCommandAsync()
    {
        await _mediat.Send(new NotifyUsersCommand());
    }
}