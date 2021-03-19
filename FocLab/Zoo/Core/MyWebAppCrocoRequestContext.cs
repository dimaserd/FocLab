using Croco.Core.Application;
using Croco.Core.Contract.Data;
using Croco.WebApplication.Models;
using System;

namespace Zoo.Core
{
    public class MyWebAppCrocoRequestContext : WebAppCrocoRequestContext
    {
        public MyWebAppCrocoRequestContext(ICrocoPrincipal principal, string uri) : base(principal, uri)
        {
        }

        public DateTime StartedOn { get; set; } = CrocoApp.Application.DateTimeProvider.Now;
    }
}