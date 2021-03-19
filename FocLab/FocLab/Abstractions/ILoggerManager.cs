using System;

namespace FocLab.Abstractions
{
    public interface ILoggerManager
    {
        void LogException(Exception ex);
    }
}