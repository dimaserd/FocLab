using System;
using System.Threading.Tasks;
using Croco.Core.Implementations.AmbientContext;
using FocLab.Abstractions;

namespace FocLab.Implementations
{
    public class ApplicationLoggerManager : ILoggerManager
    {
        public Task LogExceptionAsync(Exception ex)
        {
            var context = new SystemCrocoAmbientContext();

            context.Logger.LogException(ex);
            return Task.CompletedTask;
        }
    }
}