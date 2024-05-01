using DatapacLibrary.Infrastructure;
using DatapacLibrary.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using DatapacLibrary.Web;
using Serilog;
using Serilog.Events;
using DatapacLibrary.Web.Controllers;
using System.Text.Json;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("AppDomain", AppDomain.CurrentDomain)
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDatabaseContext();
builder.Services.AddRepositories();
builder.Services.AddHandlers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "Handled {RequestPath}";

    // Emit debug-level events instead of the defaults
    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;

    // Attach additional properties to the request completion event
    options.EnrichDiagnosticContext = async (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);

        var requestBody = await GetRequestBody(httpContext.Request);
        diagnosticContext.Set("RequestBody", requestBody);
    };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});



app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

    db.Database.Migrate();
}

app.Run();


async Task<string> GetRequestBody(HttpRequest httpRequest)
{
    string requestBody = string.Empty;
    httpRequest.EnableBuffering();

    using var reader = new Microsoft.AspNetCore.WebUtilities.HttpRequestStreamReader(httpRequest.Body, Encoding.UTF8);
    var payload = await reader.ReadToEndAsync();
    if (!string.IsNullOrEmpty(payload))
    {
        var json = JsonSerializer.Deserialize<object>(payload);
        requestBody = $"{JsonSerializer.Serialize(json)} ";
    }

    return requestBody;
}