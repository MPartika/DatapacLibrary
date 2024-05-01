using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DatapacLibrary.ApplicationCore;

public static class RegisterHandlers
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}