using RabbitMQDockerEntityExample.Core.BusinessLogic.Models.DTOs.Input;
using RabbitMQDockerEntityExample.Core.BusinessLogic;

namespace RabbitMQDockerEntityExample.Presentation.Endpoints;

public static class CalculationModule
{
    public static void RegisterCalculationEndpoints(this WebApplication app)
    {

        app.MapPost("Calculation/{key:int}", (int key, CalculationRequest body, ICalculationHandler handler) => handler.HandleCalculation(key, body));
    }
}