using Croco.Core.Data.Models.Principal;
using System.Security.Principal;

namespace Zoo
{
    public class SystemCrocoPrincipal : MyCrocoPrincipal
    {
        public SystemCrocoPrincipal(IPrincipal principal) : base(principal, SystemPrincipal.GetSystemId)
        {
        }
    }
}
