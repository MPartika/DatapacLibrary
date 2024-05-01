using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;

namespace DatapacLibrary.Infrastructure.Mappers;

public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}