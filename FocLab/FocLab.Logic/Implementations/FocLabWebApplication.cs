using Croco.Core.Contract.Application;
using Croco.Core.Contract.EventSourcing.Services;
using Croco.WebApplication.Application;
using Microsoft.AspNetCore.StaticFiles;

namespace FocLab.Logic.Implementations
{
    public class FocLabWebApplication : CrocoWebApplication
    {
        public FocLabWebApplication(ICrocoApplicationOptions opts, 
            CrocoWebApplicationOptions webOpts, EventSourcer eventSourcer) 
            : base(opts, webOpts, eventSourcer)
        {
        }

        public static string GetMimeMapping(string fileName)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);

            return contentType ?? "application/octet-stream";
        }

        public static bool IsImage(string fileName)
        {
            return GetMimeMapping(fileName).StartsWith("image/");
        }
    }
}