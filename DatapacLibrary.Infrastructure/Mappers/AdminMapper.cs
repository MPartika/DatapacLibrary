using DatapacLibrary.Domain.DataTransferObjects;
using DatapacLibrary.Infrastructure.DbEntities;

namespace DatapacLibrary.Infrastructure.Mappers;

public static class AdminMapper
{
    public static AdminDto ToAdminDto(this Admin admin)
    {
        return new AdminDto
        {
            Id = admin.Id,
            Name= admin.Name,
            Password = admin.Password,
            Salt = admin.Salt
        };
    }    
}