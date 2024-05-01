using DatapacLibrary.Infrastructure;
using DatapacLibrary.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using MediatR;
using DatapacLibrary.ApplicationCore.Queries;
using DatapacLibrary.Web;
using Microsoft.AspNetCore.Mvc;
using DatapacLibrary.ApplicationCore.Commands;

var builder = WebApplication.CreateBuilder(args);

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
