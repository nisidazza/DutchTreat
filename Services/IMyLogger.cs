using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Services
{
    public interface IMyLogger<T>
    {
        void LogError(string errorMessage);
    }
}
