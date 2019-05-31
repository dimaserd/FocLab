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
            using (var db = CrocoApp.Application.GetDbContext())
            {
                var logger = new Croco.Core.Loggers.ExceptionLogger(db, () => DateTime.Now);
                return logger.LogExceptionAsync(ex);
            }
        }
    }
}
