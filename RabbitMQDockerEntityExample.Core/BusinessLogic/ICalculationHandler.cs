using RabbitMQDockerEntityExample.Core.BusinessLogic.Models.DTOs.Input;
using RabbitMQDockerEntityExample.Core.BusinessLogic.Models.DTOs.Output;

namespace RabbitMQDockerEntityExample.Core.BusinessLogic;

public interface ICalculationHandler
{
    CalculationResult HandleCalculation(int key, CalculationRequest request);
}