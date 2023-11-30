using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQDockerEntityExample.Core.BusinessLogic.Models
{
    public record CalculationStorageItem(double Value, DateTime UpdatedAt);
}
