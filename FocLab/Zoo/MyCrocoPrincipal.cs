using Croco.Core.Data.Abstractions;
using System;
using System.Security.Principal;

namespace Zoo
{
    public class MyCrocoPrincipal : ICrocoPrincipal
    {

        private readonly Func<IPrincipal, string> _getIdFunc;

        public MyCrocoPrincipal(IPrincipal principal, Func<IPrincipal, string> getIdFunc)
        {
            UserPrincipal = principal;
            _getIdFunc = getIdFunc;
        }

        public IPrincipal UserPrincipal { get; }

        public string UserId => _getIdFunc(UserPrincipal);
    }
}
