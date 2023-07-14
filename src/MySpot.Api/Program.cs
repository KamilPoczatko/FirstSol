using MySpot.Core.Enitites;
using MySpot.Infractructure.Repositories;
using MySpot.Core.Repositories;
using MySpot.Application.Services;
using MySpot.Core.ValueObjects;
using MySpot.Infractructure;
using MySpot.Core;
using MySpot.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IClock, Clock>()
    .AddInfrastructure()
    .AddCore()
    .AddApplication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
