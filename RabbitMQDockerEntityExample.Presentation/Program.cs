using Microsoft.EntityFrameworkCore;
using RabbitMQDockerEntityExample.Core.BackgroundWorkers;
using RabbitMQDockerEntityExample.Core.BusinessLogic;
using RabbitMQDockerEntityExample.Core.BusinessLogic.Models;
using RabbitMQDockerEntityExample.Core.Messaging;
using RabbitMQDockerEntityExample.Core.Storage;
using RabbitMQDockerEntityExample.DAL;
using RabbitMQDockerEntityExample.Presentation.Endpoints;
using RabbitMQDockerEntityExample.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStorage<int, CalculationStorageItem>, Storage<int, CalculationStorageItem>>();
builder.Services.AddSingleton<ICalculationHandler, CalculationHandler>();
builder.Services.AddSingleton<Settings>();
builder.Services.AddTransient<IMessagingService, MessagingService>();
builder.Services.AddHostedService<MessagesDisplayingWorker>();
builder.Services.AddDbContext<ExampleDatabaseContext>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<ExampleDatabaseContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterCalculationEndpoints();

app.Run();
