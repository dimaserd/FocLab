using Croco.Core.Data.Abstractions;
using System;

namespace Zoo
{
    ///<inheritdoc/>
    public class MyRequestContext : IRequestContext
    {
        ///<inheritdoc/>
        public MyRequestContext(ICrocoPrincipal principal)
        {
            UserPrincipal = principal;
            RequestId = Guid.NewGuid().ToString();
        }

        ///<inheritdoc/>
        public ICrocoPrincipal UserPrincipal { get; }

        public string RequestId { get; }
    }
}
