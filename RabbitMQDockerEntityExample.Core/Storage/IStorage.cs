using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQDockerEntityExample.Core.Storage;

public interface IStorage<TKey, TValue>
{
    bool TryGetValue(TKey key, out TValue? value);
    TValue? SetValue(TKey key, TValue? value);
}