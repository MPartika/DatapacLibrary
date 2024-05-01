using DatapacLibrary.Infrastructure;
using DatapacLibrary.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using MediatR;
using DatapacLibrary.ApplicationCore.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDatabaseContext();
builder.Services.AddRepositories();
builder.Services.AddHandlers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/weatherforecast", async (IMediator mediat ) =>
{
    
    return await mediat.Send(new GetAllUsersQuery());
})
.WithName("GetWeatherForecast")
.WithOpenApi();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

    db.Database.Migrate();
}

app.Run();
