using Croco.WebApplication.Application;
using Microsoft.AspNetCore.StaticFiles;

namespace FocLab.Logic.Implementations
{
    public class FocLabWebApplication : CrocoWebApplication
    {
        public FocLabWebApplication(CrocoWebApplicationOptions options) : base(options)
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

        public string DomainName => null;

        public bool IsDevelopment { get; set; }
    }
}
