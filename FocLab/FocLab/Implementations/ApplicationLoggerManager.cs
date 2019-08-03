using System;
using System.Threading.Tasks;
using Croco.Core.Application;
using FocLab.Abstractions;

namespace FocLab.Implementations
{
    public class ApplicationLoggerManager : ILoggerManager
    {
        public Task LogExceptionAsync(Exception ex)
        {
            var logger = CrocoApp.Application.GetLogger();
            logger.LogException(ex);

            return Task.CompletedTask;
            
        }
    }
}
