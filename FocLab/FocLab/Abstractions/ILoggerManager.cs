using System;
using System.Threading.Tasks;

namespace FocLab.Abstractions
{
    public interface ILoggerManager
    {
        Task LogExceptionAsync(Exception ex);
    }
}
