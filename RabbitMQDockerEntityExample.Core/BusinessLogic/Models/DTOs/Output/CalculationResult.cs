using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RabbitMQDockerEntityExample.Core.BusinessLogic.Models.DTOs.Output;

public class CalculationResult
{
    [JsonPropertyName("computed_value")]
    public double ComputedValue { get; set; }

    [JsonPropertyName("input_value")]
    public decimal InputValue { get; set; }

    [JsonPropertyName("previous_value")]
    public double? PreviousValue { get; set; }

}