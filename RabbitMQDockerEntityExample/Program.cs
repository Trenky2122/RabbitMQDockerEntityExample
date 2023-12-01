using RabbitMQDockerEntityExample.Core.BackgroundWorkers;
using RabbitMQDockerEntityExample.Core.BusinessLogic;
using RabbitMQDockerEntityExample.Core.BusinessLogic.Models;
using RabbitMQDockerEntityExample.Core.BusinessLogic.Models.DTOs.Input;
using RabbitMQDockerEntityExample.Core.BusinessLogic.Models.DTOs.Output;
using RabbitMQDockerEntityExample.Core.Messaging;
using RabbitMQDockerEntityExample.Core.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStorage<int, CalculationStorageItem>, Storage<int, CalculationStorageItem>>();
builder.Services.AddSingleton<ICalculationHandler, CalculationHandler>();
builder.Services.AddTransient<IMessagingService, MessagingService>();
builder.Services.AddHostedService<MessagesDisplayingWorker>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("Calculation/{key:int}", (int key, CalculationRequest body, ICalculationHandler handler) => handler.HandleCalculation(key, body));


app.Run();
