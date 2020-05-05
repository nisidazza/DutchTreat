using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Services
{
    public class MyLogger<T> : IMyLogger<T>
    {
        private readonly ILogger<T> _logger;

        public MyLogger(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void LogError(string errorMessage)
        {
            _logger.LogError(errorMessage);
        }
    }
}
