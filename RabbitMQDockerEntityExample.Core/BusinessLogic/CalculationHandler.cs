﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQDockerEntityExample.Core.BusinessLogic.Models;
using RabbitMQDockerEntityExample.Core.BusinessLogic.Models.DTOs.Input;
using RabbitMQDockerEntityExample.Core.BusinessLogic.Models.DTOs.Output;
using RabbitMQDockerEntityExample.Core.Messaging;
using RabbitMQDockerEntityExample.Core.Storage;

namespace RabbitMQDockerEntityExample.Core.BusinessLogic;

public class CalculationHandler : ICalculationHandler
{
    private readonly IStorage<int, CalculationStorageItem> _storage;
    private readonly IMessagingService _messagingService;
    public CalculationHandler(IStorage<int, CalculationStorageItem> storage, IMessagingService messagingService)
    {
        _storage = storage;
        _messagingService = messagingService;
    }

    public CalculationResult HandleCalculation(int key, CalculationRequest request)
    {
        bool isValueSet = _storage.TryGetValue(key, out CalculationStorageItem? value);
        bool computeValue = isValueSet && value?.UpdatedAt > DateTime.UtcNow.AddSeconds(-15);
        double finalValue = 2;
        if(computeValue)
            finalValue = Math.Cbrt(Math.Log(decimal.ToDouble(request.Input))) / value!.Value;
        _storage.SetValue(key, new CalculationStorageItem(finalValue, DateTime.UtcNow));
        _messagingService.SendMessage(finalValue);
        return new CalculationResult()
        {
            InputValue = request.Input,
            PreviousValue = value?.Value,
            ComputedValue = finalValue
        };
    }
}