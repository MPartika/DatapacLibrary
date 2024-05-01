using DatapacLibrary.Domain.DataTransferObjects;
using MediatR;

namespace DatapacLibrary.ApplicationCore.Queries;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;
