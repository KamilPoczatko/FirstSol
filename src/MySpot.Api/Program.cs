using MySpot.Core.Enitites;
using MySpot.Infractructure.DAL.Repositories;
using MySpot.Core.Repositories;
using MySpot.Application.Services;
using MySpot.Core.ValueObjects;
using MySpot.Infractructure;
using MySpot.Core;
using MySpot.Application;
using MySpot.Infractructure.DAL;
using Microsoft.EntityFrameworkCore;

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
using(var scoper = app.Services.CreateScope())
{
    var dbContext = scoper.ServiceProvider.GetRequiredService<MySpotDbContext>();
    dbContext.Database.Migrate();

    var weeklyParkingSpots = dbContext.WeeklyParkingSpots.ToList();

    if(! weeklyParkingSpots.Any())
    {
        var clock = new Clock();
        weeklyParkingSpots = new List<WeeklyParkingSpot>()
        {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5")
        };
        dbContext.WeeklyParkingSpots.AddRange(weeklyParkingSpots);
        dbContext.SaveChanges();
    }
}


app.Run();
