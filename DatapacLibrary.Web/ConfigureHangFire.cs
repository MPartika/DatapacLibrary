using DatapacLibrary.Web.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;

namespace DatapacLibrary.ApplicationCore;

public static class ConfigureHangFire
{
    public static IServiceCollection AddHangFire(this IServiceCollection services)
    {

        services.AddHangfire(x =>
            x.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSerilogLogProvider()
                .UseColouredConsoleLogProvider()
                .UseMemoryStorage());
        services.AddHangfireServer(x => x.SchedulePollingInterval = TimeSpan.FromSeconds(5));
        services.AddTransient<NotifyUsersJob>();

        return services;
    }

    public static void EnqueueNotifyJob() 
    {
        RecurringJob.AddOrUpdate<NotifyUsersJob>("NotifyUsersJob", x => x.SendCommandAsync(), Cron.Minutely);
    }

}