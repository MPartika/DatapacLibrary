using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;

namespace DatapacLibrary.Infrastructure.Mappers;

internal static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Books = user.UserBooks?
                .Where(x => !x.Returned)
                .Select(x => x.Book?.ToBookDto())
                .ToList(),
        };
    }

    public static UserPasswordDto ToUserWithPasswordDto(this User user)
    {
        return new UserPasswordDto
        {
            Id = user.Id,
            Password = user.Password,
            Salt = user.Salt
        };
    }
}