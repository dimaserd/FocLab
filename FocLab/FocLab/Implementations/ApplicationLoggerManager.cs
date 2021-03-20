using System;
using FocLab.Abstractions;
using Microsoft.Extensions.Logging;

namespace FocLab.Implementations
{
    public class ApplicationLoggerManager : ILoggerManager
    {
        ILogger<ApplicationLoggerManager> Logger { get; }

        public ApplicationLoggerManager(ILogger<ApplicationLoggerManager> logger)
        {
            Logger = logger;
        }

        public void LogException(Exception ex)
        {
            Logger.LogError(ex, "Global error");
        }
    }
}