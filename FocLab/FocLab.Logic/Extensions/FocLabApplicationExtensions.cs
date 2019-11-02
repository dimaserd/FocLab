using Croco.Core.Abstractions;
using FocLab.Logic.Implementations;

namespace FocLab.Logic.Extensions
{
    public static class FocLabApplicationExtensions
    {
        public static bool IsDevelopment(this ICrocoApplication application)
        {
            return ((FocLabWebApplication)application).IsDevelopment;
        }
    }
}
