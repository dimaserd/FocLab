using System.Security.Principal;

namespace NewFocLab.Logic.Extensions
{
    /// <summary>
    /// Расширения
    /// </summary>
    public static class MyIdentityExtensions
    {
        public static bool IsAdmin(this IPrincipal rolePrincipal)
        {
            return rolePrincipal.IsInRole("Admin");
        }
    }
}
