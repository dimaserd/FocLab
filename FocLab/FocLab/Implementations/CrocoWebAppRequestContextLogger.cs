using Croco.Core.Abstractions;
using Croco.Core.Application;
using Croco.WebApplication.Models;
using System;

namespace FocLab.Implementations
{
    public class WebAppRequestContextLog
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string RequestId { get; set; }

        public string ParentRequestId { get; set; }

        public string Uri { get; set; }

        public DateTime StartedOn { get; set; }
    }

    public class CrocoWebAppRequestContextLogger : ICrocoRequestContextLogger
    {
        public void LogRequestContextAsync(ICrocoAmbientContext ambientContext)
        {
            
        }

        private void LogRequestToDatabase(ICrocoAmbientContext ambientContext)
        {
            var requestContext = ambientContext.RequestContext as WebAppRequestContext;

            var repo = ambientContext.RepositoryFactory.GetRepository<WebAppRequestContextLog>();

            repo.CreateHandled(new WebAppRequestContextLog
            {
                ParentRequestId = requestContext.ParentRequestId,
                RequestId = requestContext.RequestId,
                UserId = requestContext.UserPrincipal.UserId,
                Uri = requestContext.Uri,
                StartedOn = CrocoApp.Application.DateTimeProvider.Now
            });
        }
    }
}
